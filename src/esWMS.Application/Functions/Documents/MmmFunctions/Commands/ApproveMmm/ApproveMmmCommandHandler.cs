using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetSortedFilteredWarehouseUnits;
using esWMS.Application.Responses;
using FluentValidation.Results;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.ApproveMmm
{
    internal class ApproveMmmCommandHandler
        (IMmmRepository mmmRepository,
        IMmpRepository mmpRepository,
        IWarehouseUnitRepository warehouseUnitRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<ApproveMmmCommand, BaseResponse<MmmDto>>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IMmpRepository _mmpRepository = mmpRepository;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<MmmDto>> Handle
            (ApproveMmmCommand request, CancellationToken cancellationToken)
        {
            var mmmDocument = await _mmmRepository.GetDocumentByIdWithItemsAsync(request.DocumentId);

            if (mmmDocument == null)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The Document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<MmmDto>(vr);
            }

            mmmDocument.IsApproved = true;
            mmmDocument.AprovedDate = DateTime.Now;
            mmmDocument.ApprovingEmployeeId = request.ModifiedBy;
            mmmDocument.ModifiedAt = DateTime.Now;
            mmmDocument.ModifiedBy = request.ModifiedBy;

            var lastNumberMmp = await _mmpRepository.GetAllDocumentIdForDay(mmmDocument.DocumentIssueDate);

            var entityMmp = new MMP
                ("",
                issueWarehouseId: mmmDocument.ToWarehouseId,
                comment: mmmDocument.Comment,
                documentIssueDate: mmmDocument.DocumentIssueDate,
                issuingEmployeeId: null,
                assignedEmployeeId: null,
                isApproved: false,
                aprovedDate: null,
                approvingEmployeeId: null,
                goodsReceiptDate: null,
                fromWarehouseId: mmmDocument.IssueWarehouseId,
                relatedMmmId: mmmDocument.DocumentId,
                createdAt: mmmDocument.CreatedAt,
                createdBy: null,
                modifiedAt: null,
                modifiedBy: null);

            entityMmp.DocumentId = entityMmp.GenerateDocumentId(lastNumberMmp);
            var documentItmesForMmp = mmmDocument.DocumentItems.ToList();
            documentItmesForMmp.ForEach(di => di.DocumentId = Guid.NewGuid().ToString());
            entityMmp.DocumentItems = documentItmesForMmp;

            var warehouseUnitsResponse = await _mediator.Send(
                new GetSortedFilteredWarehouseUnitsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 1000,
                        Filters = "FilterByWarehouseUnitItemIds==" + string.Join('|', mmmDocument.DocumentItems.SelectMany(di => di.DocumentWarehouseUnitItems).Select(wui => wui.WarehouseUnitItemId).Distinct())
                    }));

            var warehouseUnitsToMove = _mapper.Map<List<WarehouseUnit>>(warehouseUnitsResponse.ReturnedObj?.Items) ?? [];

            if (!warehouseUnitsResponse.Success || !warehouseUnitsToMove.Any())
            {
                return new BaseResponse<MmmDto>(false, "Something went wrong.");
            }

            warehouseUnitsToMove.ForEach(wu =>
            {
                wu.WarehouseId = mmmDocument.ToWarehouseId;
                wu.Warehouse = mmmDocument.ToWarehouse;
                wu.LocationId = null;
                wu.Location = null;
            });

            MmmDto updatedMmmDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedMmm = await _mmmRepository.UpdateAsync(mmmDocument);

                var createdMmp = await _mmpRepository.CreateAsync(entityMmp);

                var movedWArehouseUnits = await _warehouseUnitRepository.UpdateWarehouseUnitsAsync(warehouseUnitsToMove.ToArray());

                await _transactionManager.CommitTransactionAsync();

                updatedMmmDto = _mapper.Map<MmmDto>(updatedMmm);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<MmmDto>(false, "Something went wrong.");
            }

            return new BaseResponse<MmmDto>(updatedMmmDto);
        }
    }
}
