using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
using FluentValidation;
using MediatR;

internal class ApprovePzItemsValidator : AbstractValidator<ApprovePzItemsCommand>
{
    private readonly IMediator _mediator;

    public ApprovePzItemsValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(x => x)
            .CustomAsync(ValidateDocumentAndItems);
    }

    private async Task ValidateDocumentAndItems(
        ApprovePzItemsCommand value,
        ValidationContext<ApprovePzItemsCommand> context,
        CancellationToken cancellationToken)
    {
        var documentResponse = await _mediator.Send(new GetPzByIdQuery(value.DocumentId));
        var document = documentResponse.ReturnedObj;

        if (!documentResponse.IsSuccess() || document == null)
        {
            context.AddFailure("DocumentId", $"The document by Id: {value} does not exist.");
            return;
        }

        ValidateDocumentItems(value, document, context);
        await ValidateWarehouseUnits(value, document, context, cancellationToken);
    }

    private void ValidateDocumentItems(
        ApprovePzItemsCommand value,
        PzDto document,
        ValidationContext<ApprovePzItemsCommand> context)
    {
        var documentItemIds = document.DocumentItems.Select(x => x.DocumentItemId).ToArray();
        var contained = value.DocumentItemsWithAssignment.Select(x => x.DocumentItemId).Except(documentItemIds);

        if (contained.Any())
        {
            context.AddFailure("DocumentItemsId", $"The original document does not contain these item identifiers: {string.Join("; ", contained)}");
        }

        foreach (var item in value.DocumentItemsWithAssignment)
        {
            var docItem = document.DocumentItems.First(x => x.DocumentItemId.Equals(item.DocumentItemId));

            if (docItem.IsApproved)
            {
                context.AddFailure("DocumentWarehouseUnitItems", $"The document item by ID: {docItem.DocumentItemId} is already approved.");
            }
        }

        foreach (var docItemId in value.DocumentItemsWithAssignment.Select(x => x.DocumentItemId))
        {
            var docItem = document.DocumentItems.First(x => x.DocumentItemId.Equals(docItemId));

            var totalQuantitySoFar = docItem.DocumentWarehouseUnitItems.Sum(x => x.Quantity);

            var warehouseUnitItemIdsContained = docItem.DocumentWarehouseUnitItems.Select(x => x.WarehouseUnitItemId);

            var newAssignmentQuantity = value.DocumentItemsWithAssignment
                .Where(x => x.DocumentItemId.Equals(docItemId))
                .Sum(x => x.Quantity);

            if (totalQuantitySoFar + newAssignmentQuantity > docItem.Quantity)
            {
                context.AddFailure(
                    "DocumentWarehouseUnitItems",
                    $"The quantity being assigned ({totalQuantitySoFar + newAssignmentQuantity}) exceeds the available quantity ({docItem.Quantity}) for the Document Item ID: {docItemId}. Warehouse Unit IDs involved: {string.Join("; ", warehouseUnitItemIdsContained)}");
            }
        }
    }

    private async Task ValidateWarehouseUnits(
        ApprovePzItemsCommand value,
        PzDto document,
        ValidationContext<ApprovePzItemsCommand> context, CancellationToken cancellationToken)
    {
        var warehouseUnitIds = value.DocumentItemsWithAssignment.Select(x => x.WarehouseUnitId!).ToArray();
        if (warehouseUnitIds.Length == 0)
        {
            context.AddFailure("WarehouseUnitIds", "No warehouse units provided.");
            return;
        }

        var warehouseUnitResponse = await _mediator.Send(new GetWarehouseUnitsByIdsQuery(warehouseUnitIds), cancellationToken);
        var warehouseUnits = warehouseUnitResponse.ReturnedObj;

        if (!warehouseUnitResponse.IsSuccess() || warehouseUnits == null || warehouseUnits.Count() == 0)
        {
            context.AddFailure("WarehouseUnitIds", "Something went wrong while fetching warehouse units.");
            return;
        }

        ValidateWarehouseUnitAssignments(value, warehouseUnits, document, context);
    }

    private void ValidateWarehouseUnitAssignments(
        ApprovePzItemsCommand value,
        IEnumerable<WarehouseUnitDto> warehouseUnits,
        PzDto document,
        ValidationContext<ApprovePzItemsCommand> context)
    {
        var warehouseUnitIdsResponse = warehouseUnits.Select(x => x.WarehouseUnitId).ToArray();
        var warehouseUnitIdsContained = value.DocumentItemsWithAssignment.Select(x => x.WarehouseUnitId!).Except(warehouseUnitIdsResponse);

        if (warehouseUnitIdsContained.Any())
        {
            context.AddFailure("WarehouseUnitIds", $"There are no warehouse unit with identifiers: {string.Join("; ", warehouseUnitIdsContained)}");
        }

        if (warehouseUnits.Any(wu => wu.WarehouseId != document.IssueWarehouseId))
        {
            var nonMatchingWarehouseUnits = warehouseUnits.Where(wu => wu.WarehouseId != document.IssueWarehouseId).ToList();
            context.AddFailure("WarehouseUnitIds", $"The following warehouse units are not members of the warehouse with ID {document.IssueWarehouseId}: {string.Join("; ", nonMatchingWarehouseUnits.Select(wu => wu.WarehouseUnitId))}");
        }

        if (warehouseUnits.Any(wu => wu.IsBlocked))
        {
            var blockedWarehouseUnits = warehouseUnits.Where(wu => wu.IsBlocked).ToList();
            context.AddFailure("WarehouseUnitIds", $"The following warehouse units are blocked: {string.Join("; ", blockedWarehouseUnits.Select(wu => wu.WarehouseUnitId))}");
        }

        ValidateMediaAssignments(value, warehouseUnits, context);
    }

    private void ValidateMediaAssignments(
        ApprovePzItemsCommand value,
        IEnumerable<WarehouseUnitDto> warehouseUnits,
        ValidationContext<ApprovePzItemsCommand> context)
    {
        foreach (var assignmentWu in value.DocumentItemsWithAssignment.Select(x => x.WarehouseUnitId).Distinct())
        {
            var isMediaAssignments = value.DocumentItemsWithAssignment.Where(x => x.WarehouseUnitId.Equals(assignmentWu) && x.IsMedia == true);
            if (isMediaAssignments.Count() > 1)
            {
                context.AddFailure("DocumentItemsWithAssignment", $"Warehouse Unit can have only one Warehouse Unit Item with set IsMediaOfWarehouseUnit on true.");
            }

            if (isMediaAssignments.Count() == 1)
            {
                var mediaItem = isMediaAssignments.First();
                var warehouseUnit = warehouseUnits.FirstOrDefault(x => x.WarehouseUnitId == mediaItem.WarehouseUnitId);

                var existingMedia = warehouseUnit?.WarehouseUnitItems.Any(x => x.IsMediaOfWarehouseUnit);
                if (existingMedia == true)
                {
                    context.AddFailure("IsMediaOfWarehouseUnit", $"Warehouse Unit by Id {warehouseUnit.WarehouseUnitId} already has a Warehouse Unit Item with IsMediaOfWarehouseUnit set to true.");
                }
            }
        }
    }
}
