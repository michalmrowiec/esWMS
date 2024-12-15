using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    internal class CreateDocumentItemValidator : AbstractValidator<CreateDocumentItemCommand>
    {
        public CreateDocumentItemValidator(IEnumerable<ProductDto> products)
        {
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var product = products.FirstOrDefault(p => p.ProductId == value.ProductId);

                    if (product == null)
                    {
                        context.AddFailure($"Product with ID {value.ProductId} does not exist.");
                        return;
                    }

                    if (!product.IsWeight && value.Quantity % 1 != 0)
                    {
                        context.AddFailure("Quantity must be a whole number for non-weight products.");
                    }
                });
        }
    }
}
