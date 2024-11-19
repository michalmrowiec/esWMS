using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw
{
    internal class CreatePwValidator : CreateBaseDocumentValidator<CreatePwCommand>
    {
        public CreatePwValidator(IEnumerable<ProductDto> productsFromDocumentItems) : base(productsFromDocumentItems)
        {

        }
    }
}
