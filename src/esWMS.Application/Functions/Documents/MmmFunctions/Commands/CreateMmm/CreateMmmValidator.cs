using esWMS.Application.Functions.WarehouseUnitItems;
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

            //RuleForEach(x => x.WarehouseUnits)
            //    .ChildRules(wu =>
            //        wu.RuleForEach(x => x.WarehouseUnitItems)
            //            .ChildRules(wui =>
            //            {
            //                wui.RuleFor(x => x.Quantity)
            //                    .GreaterThan(0);

            //                wui.RuleFor(x => x.BlockedQuantity)
            //                    .Equals(0); // not working??
            //            }));

            RuleFor(x => x.WarehouseUnits)
                .Custom((value, context) =>
                {
                    var wuis = value.SelectMany(wu => wu.WarehouseUnitItems).ToList();

                    foreach (var wui in wuis)
                    {
                        if(wui.Quantity <= 0)
                        {

                        }

                        if(wui.BlockedQuantity != 0)
                        {
                            context.AddFailure(
                                "WarehouseUnits",
                                $"Warehouse unit items can only contain items that are not locked. " +
                                $"However, the warehouse unit item with ID: {wui.WarehouseUnitItemId} has a lock on {wui.BlockedQuantity} items.");
                        }
                    }
                });

            RuleFor(x => x.ToWarehouseId)
                .NotEqual(x => x.IssueWarehouseId)
                .WithMessage("The destination warehouse cannot be the source warehouse.")
                .NotEmpty()
                .NotNull();

        }
    }
}
