using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
using esWMS.Application.Functiosns.Documents.PwFunctions.Queries.GetPwById;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePwItems
{
    internal class ApprovePwItemsValidator : AbstractValidator<ApprovePwItemsCommand>
    {
        private readonly IMediator _mediator;
        public ApprovePwItemsValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleForEach(x => x.DocumentItemsWithAssignment)
                .ChildRules(itemsASsignment =>
                    itemsASsignment.RuleFor(x => x.WarehouseUnitId)
                    .NotEmpty()
                    .NotNull());

            RuleFor(x => x)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var documentResponse = await _mediator.Send(new GetPwByIdQuery(value.DocumentId));
                    var document = documentResponse.ReturnedObj;

                    if (!documentResponse.IsSuccess() || document == null)
                    {
                        context.AddFailure("DocumentId", $"The document by Id: {value} does not exist.");
                    }

                    var documentItemIds = document.DocumentItems.Select(x => x.DocumentItemId).ToArray();
                    var contained = value.DocumentItemsWithAssignment.Select(x => x.DocumentItemId).Except(documentItemIds);

                    if (contained.Any())
                    {
                        context.AddFailure(
                            "DocumentItemsId",
                            $"The original document does not contain these item identifiers: {string.Join("; ", contained)}");
                    }

                    foreach (var item in value.DocumentItemsWithAssignment)
                    {
                        var docItem = document.DocumentItems.First(x => x.DocumentItemId.Equals(item.DocumentItemId));

                        if (docItem.IsApproved)
                        {
                            context.AddFailure(
                                "DocumentWarehouseUnitItems",
                                $"The document item by ID: {docItem.DocumentItemId} is already  approved");
                        }
                    }

                    if(value.DocumentItemsWithAssignment == null || value.DocumentItemsWithAssignment.Count == 0)
                    {
                        context.AddFailure("DocumentItemsWithAssignment", "No document items provided.");
                    }

                    if(value.DocumentItemsWithAssignment!.Any(x => x.WarehouseUnitId == null))
                    {
                        context.AddFailure("WarehouseUnitIds", "No warehouse units provided.");
                    }   

                    string[] warehouseUnitIds = value.DocumentItemsWithAssignment!
                                                    .Select(x => x.WarehouseUnitId!)
                                                    .ToArray();

                    if (warehouseUnitIds.Length != 0)
                    {
                        var warehouseUnitResponse = await _mediator.Send(
                            new GetWarehouseUnitsByIdsQuery(warehouseUnitIds));
                        var warehouseUnit = warehouseUnitResponse.ReturnedObj;

                        if (!warehouseUnitResponse.IsSuccess()
                            || warehouseUnit == null
                            || warehouseUnit.Count() == 0)
                        {
                            context.AddFailure("Somenthing went wrong");
                        }
                        else
                        {
                            var warehouseUnitIdsResponse = warehouseUnit!.Select(x => x.WarehouseUnitId).ToArray();
                            var warehouseUnitIdsContained = warehouseUnitIds.Except(warehouseUnitIdsResponse);

                            if (warehouseUnitIdsContained.Any())
                            {
                                context.AddFailure(
                                    "WarehouseUnitIds",
                                    $"There are no warehouse unit with identifiers: {string.Join("; ", warehouseUnitIdsContained)}");
                            }

                            if (warehouseUnit.Any(wu => wu.WarehouseId != document.IssueWarehouseId))
                            {
                                var nonMatchingWarehouseUnits = warehouseUnit.Where(wu => wu.WarehouseId != document.IssueWarehouseId).ToList();
                                context.AddFailure(
                                    "WarehouseUnitIds",
                                    $"The following warehouse units are not members of the warehouse with ID {document.IssueWarehouseId}: {string.Join("; ", nonMatchingWarehouseUnits.Select(wu => wu.WarehouseUnitId))}");
                            }

                            if (warehouseUnit.Any(wu => wu.IsBlocked))
                            {
                                var blockedWarehouseUnits = warehouseUnit.Where(wu => wu.IsBlocked).ToList();
                                context.AddFailure(
                                    "WarehouseUnitIds",
                                    $"The following warehouse units are blocked: {string.Join("; ", blockedWarehouseUnits.Select(wu => wu.WarehouseUnitId))}");
                            }
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
                });
        }
    }
}
