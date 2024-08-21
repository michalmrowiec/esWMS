using FluentValidation;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm
{
    internal class CreateMmmValidator : AbstractValidator<CreateMmmCommand>
    {
        public CreateMmmValidator()
        {
            RuleForEach(x => x.WarehouseUnits)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.WarehouseUnits)
                .ChildRules(wu =>
                    wu.RuleForEach(x => x.WarehouseUnitItems)
                        .ChildRules(wui =>
                        {
                            wui.RuleFor(x => x.Quantity)
                                .GreaterThan(0);

                            wui.RuleFor(x => x.BlockedQuantity)
                                .Equals(0);
                        }));



        }
    }
}
