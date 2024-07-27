using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    internal class CreatePzValidator : CreateBaseDocumentValidator<CreatePzCommand>
    {
        public CreatePzValidator()
        {
            RuleForEach(x => x.DocumentItems)
                .ChildRules(di =>
                {
                    di.RuleFor(x => x.WarehouseUnitItemId)
                    .Null();
                });
        }
    }
}
