using AutoMapper;
using esWMS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Responses;
using MediatR;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.MoveWarehouseUnitItem
{
    internal class MoveWarehouseUnitItemCommandHandler
        (IWarehouseUnitItemRepository repository,
        IWarehouseUnitRepository wuRepository,
        ITransactionManager transactionManager,
        IMapper mapper)
                : IRequestHandler<MoveWarehouseUnitItemCommand, BaseResponse<WarehouseUnitDto>>
    {
        private readonly IWarehouseUnitItemRepository _wuiRepository = repository;
        private readonly IWarehouseUnitRepository _wuRepository = wuRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<WarehouseUnitDto>> Handle(MoveWarehouseUnitItemCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new MoveWarehouseUnitItemValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitDto>(validationResult);
            }

            IEnumerable<WarehouseUnitItem> warehouseUnitItems = [];
            WarehouseUnit? warehouseUnit = null;

            try
            {
                warehouseUnitItems = await _wuiRepository.GetWarehouseUnitItemsByIdsAsync
                    (request.WarehouseUnitItemWithQuantity.Select(x => x.WarehouseUnitItemId).ToArray());

                warehouseUnit = await _wuRepository.GetByIdAsync(request.NewWarehouseUnitId);
            }
            catch (Exception)
            {
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.BadQuery, "Error retrieving warehouse unit items or warehouse unit");
            }

            if (warehouseUnitItems.Any(x => x.BlockedQuantity != 0))
            {
                return new BaseResponse<WarehouseUnitDto>
                        (BaseResponse.ResponseStatus.BadQuery, "Cannot move blocked items");
            }

            if (warehouseUnit.IsBlocked)
            {
                return new BaseResponse<WarehouseUnitDto>
                        (BaseResponse.ResponseStatus.BadQuery, "Cannot move items to a blocked warehouse unit");
            }

            IList<WarehouseUnitItem> newWarehouseUnitItems = [];

            foreach (var wuiq in request.WarehouseUnitItemWithQuantity)
            {
                var wui = warehouseUnitItems.First(x => x.WarehouseUnitItemId == wuiq.WarehouseUnitItemId);

                if (wuiq.Quantity > wui.Quantity)
                {
                    return new BaseResponse<WarehouseUnitDto>
                        (BaseResponse.ResponseStatus.BadQuery, "Cannot move more items than available");
                }
                else if (wuiq.Quantity == wui.Quantity)
                {
                    wui.WarehouseUnitId = request.NewWarehouseUnitId;
                    wui.IsMediaOfWarehouseUnit = false;
                    wui.ModifiedAt = DateTime.Now;
                    wui.ModifiedBy = request.ModifiedBy;
                }
                else if (wuiq.Quantity < wui.Quantity)
                {
                    wui.Quantity -= wuiq.Quantity;
                    wui.ModifiedAt = DateTime.Now;
                    wui.ModifiedBy = request.ModifiedBy;

                    var newWui = new WarehouseUnitItem
                    {
                        WarehouseUnitId = request.NewWarehouseUnitId,
                        WarehouseUnitItemId = WarehouseUnitIdGenerator.WarehouseUnitItemId(),
                        Quantity = wuiq.Quantity,
                        BatchLot = wui.BatchLot,
                        BestBefore = wui.BestBefore,
                        Currency = wui.Currency,
                        Price = wui.Price,
                        ProductId = wui.ProductId,
                        Unit = wui.Unit,
                        SerialNumber = wui.SerialNumber,
                        VatRate = wui.VatRate,
                        BlockedQuantity = 0,
                        IsMediaOfWarehouseUnit = false,
                        CreatedAt = DateTime.Now,
                        CreatedBy = request.ModifiedBy
                    };

                    newWarehouseUnitItems.Add(newWui);
                }
            }

            WarehouseUnitDto? warehouseUnitDto = null;

            try
            {
                await _transactionManager.BeginTransactionAsync();
                await _wuiRepository.UpdateWarehouseUnitItemsAsync(warehouseUnitItems.ToArray());
                await _wuiRepository.CreateRangeAsync(newWarehouseUnitItems);
                await _transactionManager.CommitTransactionAsync();

                var warehouseUnitWithNewItems = await _wuRepository.GetWarehouseUnitsWithItemsByIdsAsync(request.NewWarehouseUnitId);
                warehouseUnitDto = _mapper.Map<WarehouseUnitDto>(warehouseUnitWithNewItems.First());
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.BadQuery, "Error moving warehouse unit items");
            }

            return new BaseResponse<WarehouseUnitDto>(warehouseUnitDto);
        }
    }
}
