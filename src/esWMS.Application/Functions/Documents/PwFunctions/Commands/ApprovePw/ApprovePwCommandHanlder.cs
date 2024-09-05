using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePw
{
    internal class ApprovePzCommandHanlder
        (IPwRepository pwRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<ApprovePwCommand, BaseResponse<PwDto>>
    {
        private readonly IPwRepository _pwRepozitory = pwRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PwDto>> Handle(ApprovePwCommand request, CancellationToken cancellationToken)
        {
            var document = await _pwRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);

            if (document == null)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<PwDto>(vr);
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
                    //warUnitItem.WarehouseUnitItem.BlockedQuantity -= warUnitItem.WarehouseUnitItem.BlockedQuantity;
                    warUnitItem.WarehouseUnitItem.BlockedQuantity -= documentItem.DocumentWarehouseUnitItems.Where(wui => wui.WarehouseUnitItemId.Equals(warUnitItem.WarehouseUnitItemId)).Sum(dwui => dwui.Quantity);

                }
            }

            PwDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

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
