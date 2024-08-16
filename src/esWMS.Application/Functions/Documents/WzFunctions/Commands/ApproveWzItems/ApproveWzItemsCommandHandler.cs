using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWzItems
{
    internal class ApproveWzItemsCommandHandler
        (IWzRepository wzRepozitory,
        IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApproveWzItemsCommand, BaseResponse<WzDto>>
    {
        private readonly IWzRepository _wzRepozitory = wzRepozitory;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WzDto>> Handle(ApproveWzItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApproveWzItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WzDto>(validationResult);
            }

            var document = await _wzRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            var warehouseUnits = await _warehouseUnitRepository.GetWarehouseUnitsWithItemsByIdAsync(
                request.DocumentItemsWithAssignment
                .Select(x => x.WarehouseUnitId)
                .ToArray());

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var docItem = document.DocumentItems
                    .First(di => di.DocumentItemId.Equals(itemAssignment.DocumentItemId));

                if(docItem.DocumentWarehouseUnitItems.Any())
                {
                    if (docItem.DocumentWarehouseUnitItems.Sum(x => x.Quantity) < docItem.Quantity)
                    {

                        //...
                    }
                }

                var warUnitItem = warehouseUnits
                    .First(wu => wu.WarehouseUnitId.Equals(itemAssignment.WarehouseUnitId))
                    .WarehouseUnitItems
                    .First(wui => wui.WarehouseUnitItemId.Equals(itemAssignment.WarehouseUnitItemId));

                warUnitItem.BlockedQuantity += itemAssignment.Quantity;

                var newDocumentWarehouseUnitItem = new DocumentWarehouseUnitItem
                {
                    DocumentItemId = docItem.DocumentItemId,
                    WarehouseUnitItemId = itemAssignment.WarehouseUnitItemId!,
                    Quantity = itemAssignment.Quantity,
                    CreatedAt = DateTime.Now,
                    CreatedBy = request.ModifiedBy
                };

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

            WzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedWarehouseUnits = await _warehouseUnitRepository.UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());
                var updatedDocument = await _wzRepozitory.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<WzDto>(updatedDocument);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<WzDto>(false, "Something went wrong.");
            }

            return new BaseResponse<WzDto>(mappedUpdatedDocument);
        }
    }
}
