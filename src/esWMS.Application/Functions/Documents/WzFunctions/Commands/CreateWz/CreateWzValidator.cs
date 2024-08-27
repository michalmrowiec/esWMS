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
        public CreateWzValidator(IList<ProductDto> productsFromDocumentItems, IMediator mediator)
        {
            _mediator = mediator;
            // TODO add WarehouseUnit is no blocked check
            RuleForEach(x => x.DocumentItems)
                .ChildRules(items => items.RuleForEach(x => x.DocumentWarehouseUnitItems)
                    .ChildRules(itemsASsignment =>
                        itemsASsignment.RuleFor(x => x.WarehouseUnitItemId)
                        .NotEmpty()
                        .NotNull()));

            RuleFor(x => x.DocumentItems)
                .Custom((value, context) =>
                {
                    var requestedProductIds = value.Select(x => x.ProductId).Distinct().ToList();
                    var foundProductIds = productsFromDocumentItems?.Select(x => x.ProductId).Distinct().ToList() ?? new List<string>();

                    var missingProductIds = requestedProductIds.Except(foundProductIds).ToList();

                    if (missingProductIds.Any())
                    {
                        context.AddFailure(
                            "DocumentItems",
                            $"There are no products with the given IDs: {string.Join(", ", missingProductIds)}");
                    }
                });

            RuleFor(x => x.DocumentItems)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var allWarehouseUnitItemIds = value
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

                    foreach (var documentItem in value)
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

                            if(documentItem.ProductId != wui.ProductId)
                            {
                                context.AddFailure(
                                    "DocumentWarehouseUnitItems",
                                    $"Warehouse unit item with ID {wui.WarehouseUnitItemId} cannot be linked to document item because they do not contain the same product. " +
                                    $"The document item contains product with ID {documentItem.ProductId}, while the warehouse unit item contains product with ID {wui.ProductId}.");
                            }
                        }
                    }
                });
        }
    }
}
