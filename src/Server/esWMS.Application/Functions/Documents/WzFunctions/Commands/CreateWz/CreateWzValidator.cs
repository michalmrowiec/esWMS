using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.WarehouseUnitItems.Queries.GetSortedFilteredWarehouseUnitItems;
using FluentValidation;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz
{
    internal class CreateWzValidator : CreateBaseDocumentValidator<CreateWzCommand>
    {
        private readonly IMediator _mediator;
        public CreateWzValidator(IEnumerable<ProductDto> productsFromDocumentItems, IMediator mediator) : base(productsFromDocumentItems)
        {
            _mediator = mediator;

            RuleForEach(x => x.DocumentItems)
                .ChildRules(items => items.RuleForEach(x => x.DocumentWarehouseUnitItems)
                    .ChildRules(itemsASsignment =>
                        itemsASsignment.RuleFor(x => x.WarehouseUnitItemId)
                        .NotEmpty()
                        .NotNull()));

            RuleFor(x => x)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var allWarehouseUnitItemIds = value.DocumentItems
                                                    .SelectMany(x => x.DocumentWarehouseUnitItems)
                                                    .Select(x => x.WarehouseUnitItemId)
                                                    .Distinct()
                                                    .ToList() ?? [];

                    var warehouseUnitItemsResponse = await _mediator.Send(
                        new GetSortedFilteredWarehouseUnitItemsQuery(
                            new SieveModel()
                            {
                                Page = 1,
                                PageSize = 500,
                                Filters = "WarehouseUnitItemId==" + string.Join('|', allWarehouseUnitItemIds)
                            }));

                    var warehouseUnitItems = warehouseUnitItemsResponse.ReturnedObj?.Items ?? [];

                    if (!warehouseUnitItemsResponse.IsSuccess())
                    {
                        context.AddFailure("DocumentItems", "Something went wrong.");
                    }

                    foreach (var documentItem in value.DocumentItems)
                    {
                        foreach (var itemAssignment in documentItem.DocumentWarehouseUnitItems.DistinctBy(x => x.WarehouseUnitItemId))
                        {
                            var wui = warehouseUnitItems.First(x => x.WarehouseUnitItemId.Equals(itemAssignment.WarehouseUnitItemId));
                            var availableQuantity = wui.Quantity - wui.BlockedQuantity;

                            var allWantQuantity = documentItem.DocumentWarehouseUnitItems
                                                    .Where(x => x.WarehouseUnitItemId!.Equals(itemAssignment.WarehouseUnitItemId))
                                                    .Sum(x => x.Quantity);

                            if (availableQuantity < allWantQuantity)
                            {
                                context.AddFailure(
                                    "DocumentWarehouseUnitItems",
                                    $"Available quantity for unit {itemAssignment.WarehouseUnitItemId} is {availableQuantity} but trying to lock value {allWantQuantity}. Value exceeds by {allWantQuantity - availableQuantity}.");
                            }

                            if (documentItem.ProductId != wui.ProductId)
                            {
                                context.AddFailure(
                                    "DocumentWarehouseUnitItems",
                                    $"Warehouse unit item with ID {wui.WarehouseUnitItemId} cannot be linked to document item because they do not contain the same product. " +
                                    $"The document item contains product with ID {documentItem.ProductId}, while the warehouse unit item contains product with ID {wui.ProductId}.");
                            }
                        }
                    }

                    var warehouseUnits = warehouseUnitItems.Select(x => x.WarehouseUnit).DistinctBy(x => x?.WarehouseUnitId).ToList();

                    if (warehouseUnits == null
                        || warehouseUnits.Count == 0)
                    {
                        context.AddFailure("Somenthing went wrong");
                    }

                    if (warehouseUnits!.Any(wu => wu!.WarehouseId != value.IssueWarehouseId))
                    {
                        var nonMatchingWarehouseUnits = warehouseUnits!.Where(wu => wu?.WarehouseId != value.IssueWarehouseId).ToList();
                        context.AddFailure(
                        "WarehouseUnitIds",
                            $"The following warehouse units are not members of the warehouse with ID {value.IssueWarehouseId}: {string.Join("; ", nonMatchingWarehouseUnits.Select(wu => wu!.WarehouseUnitId))}");
                    }

                    if (warehouseUnits!.Any(wu => wu!.IsBlocked))
                    {
                        var blockedWarehouseUnits = warehouseUnits!.Where(wu => wu!.IsBlocked).ToList();
                        context.AddFailure(
                            "WarehouseUnitIds",
                            $"The following warehouse units are blocked: {string.Join("; ", blockedWarehouseUnits.Select(wu => wu.WarehouseUnitId))}");
                    }
                });
        }
    }
}
