using FluentValidation;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command
{
    internal class CreateFlatBaseDocumentValidator<T> : AbstractValidator<T>
        where T : CreateFlatBaseDocumentCommand
    {
        public CreateFlatBaseDocumentValidator()
        {

        }
    }
}