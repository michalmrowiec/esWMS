using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePwItems
{
    internal class ApprovePwItemsCommandHandler
        (IPwRepository pwRepozitory,
        IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApprovePwItemsCommand, BaseResponse<PwDto>>
    {
        private readonly IPwRepository _pwRepozitory = pwRepozitory;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<PwDto>> Handle
            (ApprovePwItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApprovePwItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PwDto>(validationResult);
            }

            var document = await _pwRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            var warehouseUnits = await _warehouseUnitRepository.GetWarehouseUnitsWithItemsByIdAsync(
                request.DocumentItemsWithAssignment!
                .Select(x => x.WarehouseUnitId!)
                .ToArray());

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var docItem = document.DocumentItems
                    .First(di => di.DocumentItemId.Equals(itemAssignment.DocumentItemId));

                var warUnit = warehouseUnits
                    .First(wu => wu.WarehouseUnitId.Equals(itemAssignment.WarehouseUnitId));

                var newWarehouseUnitItem = new WarehouseUnitItem(
                    warehouseUnitId: warUnit.WarehouseId,
                    productId: docItem.ProductId,
                    quantity: itemAssignment.Quantity,
                    blockedQuantity: itemAssignment.Quantity,
                    bestBefore: docItem.BestBefore,
                    batchLot: docItem.BatchLot,
                    serialNumber: docItem.SerialNumber,
                    price: docItem.Price,
                    createdBy: request.ModifiedBy);

                var newDocumentWarehouseUnitItem = new DocumentWarehouseUnitItem
                {
                    DocumentItemId = docItem.DocumentItemId,
                    WarehouseUnitItemId = newWarehouseUnitItem.WarehouseUnitItemId,
                    Quantity = itemAssignment.Quantity,
                    CreatedAt = DateTime.Now,
                    CreatedBy = request.ModifiedBy
                };

                warUnit.WarehouseUnitItems.Add(newWarehouseUnitItem);
                docItem.DocumentWarehouseUnitItems.Add(newDocumentWarehouseUnitItem);
            }

            foreach (var documentItem in document.DocumentItems)
            {
                var totalQuantitySoFar = documentItem.DocumentWarehouseUnitItems.Sum(x => x.Quantity);

                if (totalQuantitySoFar == documentItem.Quantity)
                {
                    documentItem.IsApproved = true;
                    documentItem.ModifiedBy = request.ModifiedBy;
                    documentItem.ModifiedAt = DateTime.Now;
                }
            }

            PwDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedWarehouseUnits = await _warehouseUnitRepository
                    .UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());

                var updatedDocument = await _pwRepozitory.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<PwDto>(updatedDocument);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<PwDto>(mappedUpdatedDocument);
        }
    }
}
