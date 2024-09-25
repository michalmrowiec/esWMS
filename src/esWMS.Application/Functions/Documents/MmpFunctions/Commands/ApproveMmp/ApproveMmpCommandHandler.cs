using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Commands.ApproveMmp
{
    internal class ApproveMmpCommandHandler
        (IMmmRepository mmmRepository,
        IMmpRepository mmpRepository,
        IWarehouseUnitRepository warehouseUnitRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<ApproveMmpCommand, BaseResponse<MmpDto>>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IMmpRepository _mmpRepository = mmpRepository;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<MmpDto>> Handle(ApproveMmpCommand request, CancellationToken cancellationToken)
        {
            MMP mmpDocument;
            try
            {
                mmpDocument = await _mmpRepository.GetDocumentByIdWithItemsAsync(request.DocumentId);
            }
            catch (KeyNotFoundException ex)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The Document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<MmpDto>(vr);
            }
            catch (Exception ex)
            {
                throw;
            }

            mmpDocument.IsApproved = true;
            mmpDocument.AprovedDate = DateTime.Now;
            mmpDocument.ApprovingEmployeeId = request.ModifiedBy;
            mmpDocument.ModifiedAt = DateTime.Now;
            mmpDocument.ModifiedBy = request.ModifiedBy;

            List<WarehouseUnit> warehouseUnitsToUnblock;
            try
            {
                //warehouseUnitsToMove = _mapper.Map<List<WarehouseUnit>>(warehouseUnitsToMoveDto) ?? [];
                warehouseUnitsToUnblock = mmpDocument
                    .DocumentItems
                    .SelectMany(di => di.DocumentWarehouseUnitItems)
                    .Select(dwui => dwui.WarehouseUnitItem)
                    .Select(wui => wui!.WarehouseUnit).ToList()!;

            }
            catch (Exception ex)
            {

                throw;
            }

            if (/*!warehouseUnitsResponse.Success ||*/ !warehouseUnitsToUnblock.Any())
            {
                return new BaseResponse<MmpDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            MmpDto updatedMmpDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedMmp = await _mmpRepository.UpdateAsync(mmpDocument);

                var warehouseUnits = await _warehouseUnitRepository
                    .SetWarehouseUnitsBlockedStatusAsync(false, warehouseUnitsToUnblock.Select(wu => wu.WarehouseUnitId).ToArray());

                await _transactionManager.CommitTransactionAsync();

                updatedMmpDto = _mapper.Map<MmpDto>(updatedMmp);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<MmpDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmpDto>(updatedMmpDto);

        }
    }
}
