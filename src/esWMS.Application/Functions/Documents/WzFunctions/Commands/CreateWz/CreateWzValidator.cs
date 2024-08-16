using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz
{
    internal class CreateWzValidator : CreateBaseDocumentValidator<CreateWzCommand>
    {
        public CreateWzValidator()
        {
            RuleForEach(x => x.DocumentItems)
                .ChildRules(items => items.RuleForEach(x => x.DocumentItemsWithAssignment)
                    .ChildRules(itemsASsignment =>
                        itemsASsignment.RuleFor(x => x.WarehouseUnitItemId)
                        .NotEmpty()
                        .NotNull()));
        }
    }
}
