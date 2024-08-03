using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems
{
    internal class ApprovePzItemsCommandHandler
        (IPzRepository pzRepozitory,
        IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApprovePzItemsCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepository _pzRepozitory = pzRepozitory;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<PzDto>> Handle(ApprovePzItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApprovePzItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PzDto>(validationResult);
            }

            var document = await _pzRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            var warehouseUnits = await _warehouseUnitRepository.GetWarehouseUnitsByIds(
                request.DocumentItemsWithAssignment
                .Select(x => x.WarehouseUnitId)
                .ToArray());

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var docItem = document.DocumentItems
                    .First(di => di.DocumentId.Equals(itemAssignment.DocumentItemId));

                docItem.IsApproved = true;
                docItem.WarehouseUnitItemId = itemAssignment.WarehouseUnitId;
                docItem.ModifiedBy = request.ModifiedBy;
                docItem.ModifiedAt = DateTime.Now;

                var warUnit = warehouseUnits
                    .First(wu => wu.WarehouseUnitId.Equals(itemAssignment.WarehouseUnitId));

                warUnit.ModifiedBy = request.ModifiedBy;
                warUnit.ModifiedAt = DateTime.Now;
                warUnit.WarehouseUnitItems.Add(
                    new WarehouseUnitItem(
                        warUnit.WarehouseId,
                        docItem.ProductId,
                        docItem.Quantity,
                        0,
                        docItem.BestBefore,
                        docItem.BatchLot,
                        docItem.SerialNumber,
                        docItem.Price,
                        request.ModifiedBy));
            }

            PzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedDocument = await _pzRepozitory.UpdateAsync(document);
                var updatedWarehouseUnit = await _warehouseUnitRepository.UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<PzDto>(updatedDocument);
            }
            catch (Exception)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<PzDto>(false, "Something went wrong.");
            }

            return new BaseResponse<PzDto>(mappedUpdatedDocument);
        }
    }
}
