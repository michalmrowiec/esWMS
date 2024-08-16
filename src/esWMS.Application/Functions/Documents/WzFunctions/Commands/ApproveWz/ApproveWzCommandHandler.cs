using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWz
{
    internal class ApproveWzCommandHandler
        (IWzRepository wzRepozitory,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<ApproveWzCommand, BaseResponse<WzDto>>
    {
        private readonly IWzRepository _wzRepozitory = wzRepozitory;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<WzDto>> Handle(ApproveWzCommand request, CancellationToken cancellationToken)
        {
            var document = await _wzRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            //var warehouseUnitItems = await _warehouseUnitItemRepository.GetWarehouseUnitItemsByIdsAsync(
            //    document.DocumentItems
            //    .Select(x => x.WarehouseUnitItemId)
            //    .OfType<string>()
            //    .ToArray());

            if (document == null)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<WzDto>(vr);
            }

            document.ApprovingEmployeeId = request.ModifiedBy;
            document.IsApproved = true;
            document.AprovedDate = DateTime.Now;
            document.ModifiedAt = DateTime.Now;
            document.ModifiedBy = request.ModifiedBy;

            foreach (var documentItem in document.DocumentItems)
            {
                foreach (var warUnitItem in documentItem.DocumentWarehouseUnitItems)
                {
                    warUnitItem.WarehouseUnitItem.Quantity -= warUnitItem.Quantity;
                    warUnitItem.WarehouseUnitItem.BlockedQuantity -= warUnitItem.WarehouseUnitItem.BlockedQuantity;
                }
            }

            WzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedDocument = await _wzRepozitory.UpdateAsync(document);
                //var updatedWarehouseUnitItems = await _warehouseUnitItemRepository
                //.UpdateWarehouseUnitItemsAsync(warehouseUnitItems.ToArray());

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
