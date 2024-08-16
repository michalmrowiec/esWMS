using esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWzItems
{
    internal class ApproveWzItemsValidator : AbstractValidator<ApproveWzItemsCommand>
    {
        private readonly IMediator _mediator;
        public ApproveWzItemsValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var documentResponse = await _mediator.Send(new GetWzByIdQuery(value.DocumentId));
                    var document = documentResponse.ReturnedObj;

                    if (!documentResponse.Success || document == null)
                    {
                        context.AddFailure("DocumentId", $"The document by Id: {value} does not exist.");
                    }

                    var documentItemIds = document.DocumentItems.Select(x => x.DocumentItemId).ToArray();
                    var contained = value.DocumentItemsWithAssignment
                                        .Select(x => x.DocumentItemId)
                                        .Except(documentItemIds);

                    if (contained.Any())
                    {
                        context.AddFailure(
                            "DocumentItemsId",
                            $"The original document does not contain these item identifiers: {string.Join("; ", contained)}");
                    }


                    // TODO already  approve check add

                    foreach (var item in value.DocumentItemsWithAssignment)
                    {
                        var docItem = document.DocumentItems.First(x => x.DocumentItemId.Equals(item.DocumentItemId));

                        if (docItem.IsApproved)
                        {
                            context.AddFailure(
                                "DocumentItemsWithAssignment",
                                $"The document item by ID: {docItem.DocumentItemId} is already  approved");
                        }
                    }


                    string[] warehouseUnitIds = value.DocumentItemsWithAssignment
                                                    .Select(x => x.WarehouseUnitId)
                                                    .ToArray();

                    if (warehouseUnitIds.Length != 0)
                    {
                        var warehouseUnitResponse = await _mediator.Send(
                            new GetWarehouseUnitsByIdsQuery(warehouseUnitIds));
                        var warehouseUnit = warehouseUnitResponse.ReturnedObj;

                        if (!warehouseUnitResponse.Success || warehouseUnit == null)
                        {
                            context.AddFailure("Somenthing went wrong");
                        }
                        // TODO add check warehouse unit is member of issue warehouse
                        var warehouseUnitItemIdsResponse = warehouseUnit!.Select(x => x.WarehouseUnitId).ToArray();

                        var warehouseUnitIdsContained = warehouseUnitIds.Except(warehouseUnitItemIdsResponse);

                        if (warehouseUnitIdsContained.Any())
                        {
                            context.AddFailure(
                                "WarehouseUnitIds",
                                $"There are no warehouse unit with identifiers: {string.Join("; ", warehouseUnitIdsContained)}");
                        }
                    }

                    // TODO fix this and add validation for warehouseUnitItems

                    //foreach (var docItemId in value.DocumentItemWithAssignment.Select(x => x.DocumentItemId))
                    //{
                    //    var docItem = document.DocumentItems.First(x => x.DocumentItemId.Equals(docItemId));

                    //    var totalQuantitySoFar = docItem.DocumentWarehouseUnitItems.Sum(x => x.Quantity);

                    //    var warehouseUnitItemIdsContained = docItem.DocumentWarehouseUnitItems.Select(x => x.WarehouseUnitItemId);

                    //    var newAssignmentQuantity = value.DocumentItemWithAssignment
                    //        .Where(x => x.DocumentItemId.Equals(docItemId))
                    //        .Sum(x => x.Quantity);

                    //    if (totalQuantitySoFar + newAssignmentQuantity > docItem.Quantity)
                    //    {
                    //        context.AddFailure(
                    //            "DocumentItemsWithAssignment",
                    //            $"The quantity being assigned ({totalQuantitySoFar + newAssignmentQuantity}) exceeds the available quantity ({docItem.Quantity}) for the Document Item ID: {docItemId}. Warehouse Unit IDs involved: {string.Join("; ", warehouseUnitItemIdsContained)}");
                    //    }
                    //}
                });
        }
    }
}
