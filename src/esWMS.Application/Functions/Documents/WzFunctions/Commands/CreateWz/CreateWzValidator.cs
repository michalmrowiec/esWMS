using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Products;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz
{
    internal class CreateWzValidator : CreateBaseDocumentValidator<CreateWzCommand>
    {
        public CreateWzValidator(IList<ProductDto> productsFromDocumentItems)
        {
            RuleForEach(x => x.DocumentItems)
                .ChildRules(items => items.RuleForEach(x => x.DocumentWarehouseUnitItems)
                    .ChildRules(itemsASsignment =>
                        itemsASsignment.RuleFor(x => x.WarehouseUnitItemId)
                        .NotEmpty()
                        .NotNull()));

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var requestedProductIds = value.DocumentItems.Select(x => x.ProductId).Distinct().ToList();
                    var foundProductIds = productsFromDocumentItems?.Select(x => x.ProductId).Distinct().ToList() ?? new List<string>();

                    var missingProductIds = requestedProductIds.Except(foundProductIds).ToList();

                    if (missingProductIds.Any())
                    {
                        context.AddFailure(
                            "ProductId",
                            $"There are no products with the given ids: {string.Join(", ", missingProductIds)}");
                    }
                });
        }
    }
}
