﻿using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZwItems
{
    internal class ApproveZwItemsCommandHandler
        (IZwRepository repozitory,
        IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApproveZwItemsCommand, BaseResponse<ZwDto>>
    {
        private readonly IZwRepository _repozitory = repozitory;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<ZwDto>> Handle
            (ApproveZwItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApproveZwItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ZwDto>(validationResult);
            }

            var document = await _repozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            var warehouseUnits = await _warehouseUnitRepository.GetWarehouseUnitsWithItemsByIdsAsync(
                request.DocumentItemsWithAssignment!
                .Select(x => x.WarehouseUnitId!)
                .ToArray());

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var docItem = document.DocumentItems
                    .First(di => di.DocumentItemId.Equals(itemAssignment.DocumentItemId));

                var warUnit = warehouseUnits
                    .First(wu => wu.WarehouseUnitId.Equals(itemAssignment.WarehouseUnitId));

                var newWarehouseUnitItem =
                    WarehouseUnitItemService.CreateWarehouseUnitItem(warUnit, docItem, itemAssignment, request.ModifiedBy);
                var newDocumentWarehouseUnitItem =
                    WarehouseUnitItemService.CreateDocumentWarehouseUnitItem(docItem, newWarehouseUnitItem, itemAssignment, request.ModifiedBy);

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

            ZwDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedWarehouseUnits = await _warehouseUnitRepository
                    .UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());

                var updatedDocument = await _repozitory.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<ZwDto>(updatedDocument);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<ZwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<ZwDto>(mappedUpdatedDocument);
        }
    }
}
