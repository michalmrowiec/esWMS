using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command
{
    internal abstract class CreateBaseDocumentValidator<T> : CreateFlatBaseDocumentValidator<T>
        where T : CreateBaseDocumentCommand
    {
        public CreateBaseDocumentValidator(IEnumerable<ProductDto> products)
        {
            RuleFor(x => x.DocumentItems)
                .NotNull().WithMessage("DocumentItems cannot be null.")
                .NotEmpty().WithMessage("DocumentItems cannot be an empty list.")
                .Custom((value, context) =>
                {
                    if (value.Count > 200)
                    {
                        context.AddFailure("DocumentItems", "Document Items count cannot exceed 200.");
                    }
                });

            RuleForEach(x => x.DocumentItems)
                .SetValidator(new CreateDocumentItemValidator(products));

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var productIdsFromRequest = value.DocumentItems.Select(x => x.ProductId).Distinct();
                    var productIdsFromDb = products?.Select(x => x.ProductId).Distinct() ?? [];

                    var missingProductIds = productIdsFromRequest.Except(productIdsFromDb);

                    if (missingProductIds.Any())
                    {
                        context.AddFailure(
                            "DocumentItems",
                            $"There are no products with the given ids: {string.Join(", ", missingProductIds)}");
                    }
                });
        }
    }
}
