using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Documents.RwFunctions.Queries.GetSortedFilteredRw;
using esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetSortedFilteredZw;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.WarehouseUnitItems.Queries.GetSortedFilteredWarehouseUnitItems;
using FluentValidation;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.CreateRw
{
    internal class CreateRwValidator : CreateBaseDocumentValidator<CreateRwCommand>
    {
        private readonly IMediator _mediator;
        public CreateRwValidator(IList<ProductDto> productsFromDocumentItems, IMediator mediator)
        {
            _mediator = mediator;
            RuleForEach(x => x.DocumentItems)
                .ChildRules(items => items.RuleForEach(x => x.DocumentWarehouseUnitItems)
                    .ChildRules(itemsASsignment =>
                        itemsASsignment.RuleFor(x => x.WarehouseUnitItemId)
                        .NotEmpty()
                        .NotNull()));

            RuleFor(x => x.DocumentItems)
                .CustomAsync(async (value, context, cancellationToken) =>
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


                    var oneYearAgo = DateTime.UtcNow.AddYears(-1).ToString("yyyy-MM-dd");

                    foreach (var documentItem in value)
                    {
                        var productId = documentItem.ProductId;

                        var rwContainstSameProductsResponse = await _mediator.Send(
                            new GetSortedFilteredRwQuery(
                                new SieveModel()
                                {
                                    Page = 1,
                                    PageSize = 500,
                                    Filters = $"ContainsProductIds=={productId};" +
                                              $"DocumentIssueDate>={oneYearAgo};" +
                                              "IsApproved==true"
                                }));
                        var rwContainstSameProducts = rwContainstSameProductsResponse.ReturnedObj?.Items ?? [];

                        var zwContainstSameProductsResponse = await _mediator.Send(
                            new GetSortedFilteredZwQuery(
                                new SieveModel()
                                {
                                    Page = 1,
                                    PageSize = 500,
                                    Filters = $"ContainsProductIds=={productId};" +
                                              $"DocumentIssueDate>={oneYearAgo};" +
                                              "IsApproved==true"
                                }));
                        var zwContainstSameProducts = zwContainstSameProductsResponse.ReturnedObj?.Items ?? [];

                        if (!rwContainstSameProductsResponse.IsSuccess()
                            || !zwContainstSameProductsResponse.IsSuccess())
                        {
                            context.AddFailure("DocumentItems", $"Something went wrong while getting product ID {productId}.");
                            continue;
                        }

                        var totalIssuedQuantity = rwContainstSameProducts.Sum(doc => doc.DocumentItems
                            .Where(di => di.ProductId == productId)
                            .Sum(item => item.Quantity));

                        var totalReturnedQuantity = zwContainstSameProducts.Sum(doc => doc.DocumentItems
                            .Where(di => di.ProductId == productId)
                            .Sum(item => item.Quantity));

                        var currentReturnQuantity = documentItem.DocumentWarehouseUnitItems.Sum(item => item.Quantity);

                        if (currentReturnQuantity > (totalIssuedQuantity - totalReturnedQuantity))
                        {
                            context.AddFailure(
                                "DocumentItems",
                                $"The quantity of product ID {productId} to be returned ({currentReturnQuantity}) exceeds the available quantity ({totalIssuedQuantity - totalReturnedQuantity}) issued by RW.");
                        }
                    }

                });

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
