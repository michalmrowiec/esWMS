using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    internal class CreatePzValidator : CreateBaseDocumentValidator<CreatePzCommand>
    {
        public CreatePzValidator(IEnumerable<ProductDto> productsFromDocumentItems) : base(productsFromDocumentItems)
        {

        }
    }
}
