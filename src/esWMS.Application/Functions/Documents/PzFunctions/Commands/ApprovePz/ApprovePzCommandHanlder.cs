﻿using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePz
{
    internal class ApprovePzCommandHanlder
        (IPzRepository pzRepozitory,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<ApprovePzCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepository _pzRepozitory = pzRepozitory;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PzDto>> Handle(ApprovePzCommand request, CancellationToken cancellationToken)
        {
            var document = await _pzRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            var warehouseUnitItems = await _warehouseUnitItemRepository.GetWarehouseUnitItemsByIdsAsync(
                document.DocumentItems
                .Select(x => x.WarehouseUnitItemId)
                .OfType<string>()
                .ToArray());

            if (document == null)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<PzDto>(vr);
            }

            document.ApprovingEmployeeId = request.ModifiedBy;
            document.IsApproved = true;
            document.AprovedDate = DateTime.Now;
            document.ModifiedAt = DateTime.Now;
            document.ModifiedBy = request.ModifiedBy;

            foreach (var warUnitItem in warehouseUnitItems)
            {
                warUnitItem.BlockedQuantity -= warUnitItem.BlockedQuantity;
            }

            PzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedDocument = await _pzRepozitory.UpdateAsync(document);
                var updatedWarehouseUnitItems = await _warehouseUnitItemRepository
                    .UpdateWarehouseUnitItemsAsync(warehouseUnitItems.ToArray());

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
