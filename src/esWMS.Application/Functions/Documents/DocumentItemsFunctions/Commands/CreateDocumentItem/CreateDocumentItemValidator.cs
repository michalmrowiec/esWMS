using FluentValidation;

namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    internal class CreateDocumentItemValidator : AbstractValidator<CreateDocumentItemCommand>
    {
        public CreateDocumentItemValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
