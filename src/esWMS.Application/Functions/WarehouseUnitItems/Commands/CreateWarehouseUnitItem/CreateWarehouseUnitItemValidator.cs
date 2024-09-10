using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem
{
    internal class CreateWarehouseUnitItemValidator : AbstractValidator<CreateWarehouseUnitItemCommand>
    {
        public CreateWarehouseUnitItemValidator(bool warehouseUnitIdCheck, IMediator mediator)
        {
            if (warehouseUnitIdCheck)
            {
                RuleFor(x => x.WarehouseUnitId)
                    .NotNull()
                    .NotEmpty();
            }

            RuleFor(x => x)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var warehouseUnitResponse = await mediator.Send(new GetWarehouseUnitsByIdsQuery(value.WarehouseUnitId));
                    var warehouseUnit = warehouseUnitResponse.ReturnedObj?.FirstOrDefault();

                    if (!warehouseUnitResponse.IsSuccess() || warehouseUnit == null)
                    {
                        context.AddFailure("WarehouseUnitId", "WarehouseUnitId does not exist.");
                        return;
                    }

                    var existMedia = warehouseUnit.WarehouseUnitItems.Where(x => x.IsMediaOfWarehouseUnit);

                    if (existMedia.Any())
                    {
                        context.AddFailure(
                            "IsMediaOfWarehouseUnit",
                            $"Warehouse Unit by Id {warehouseUnit.WarehouseUnitId} already have Warehouse Unit Item with IsMediaOfWarehouseUnit set on true. ");
                    }
                });
        }
    }
}
