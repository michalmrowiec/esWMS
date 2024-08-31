using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command
{
    internal abstract class CreateBaseDocumentValidator<T> : CreateFlatBaseDocumentValidator<T>
        where T : CreateBaseDocumentCommand
    {
        public CreateBaseDocumentValidator()
        {
            RuleForEach(x => x.DocumentItems)
                .SetValidator(new CreateDocumentItemValidator());
        }
    }
}
