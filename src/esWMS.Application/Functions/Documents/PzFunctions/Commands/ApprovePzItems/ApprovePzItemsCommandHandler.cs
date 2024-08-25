using AutoMapper;
using esMWS.Domain.Entities.Documents;
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
            var warehouseUnits = await _warehouseUnitRepository.GetWarehouseUnitsWithItemsByIdAsync(
                request.DocumentItemsWithAssignment
                .Select(x => x.WarehouseUnitId)
                .ToArray());

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var docItem = document.DocumentItems
                    .First(di => di.DocumentItemId.Equals(itemAssignment.DocumentItemId));

                //docItem.IsApproved = true;
                //docItem.ModifiedBy = request.ModifiedBy;
                //docItem.ModifiedAt = DateTime.Now;

                var warUnit = warehouseUnits
                    .First(wu => wu.WarehouseUnitId.Equals(itemAssignment.WarehouseUnitId));

                //warUnit.ModifiedBy = request.ModifiedBy;
                //warUnit.ModifiedAt = DateTime.Now;
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

            PzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedWarehouseUnits = await _warehouseUnitRepository.UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());
                var updatedDocument = await _pzRepozitory.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<PzDto>(updatedDocument);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<PzDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<PzDto>(mappedUpdatedDocument);
        }
    }
}
