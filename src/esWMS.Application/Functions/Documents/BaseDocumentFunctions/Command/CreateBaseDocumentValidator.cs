using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command
{
    internal abstract class CreateBaseDocumentValidator<T> : AbstractValidator<T>
        where T : CreateBaseDocumentCommand
    {
        public CreateBaseDocumentValidator()
        {
            RuleForEach(x => x.DocumentItems)
                .SetValidator(new CreateDocumentItemValidator());
        }
    }
}
