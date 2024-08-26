using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    internal class CreatePzValidator : CreateBaseDocumentValidator<CreatePzCommand>
    {
        public CreatePzValidator(IList<ProductDto> productsFromDocumentItems)
        {
            RuleFor(x => x.DocumentItems)
                .NotEmpty()
                .WithMessage("DocumentItems cannot be empty");

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
