using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Domain.Services;
using FluentValidation.Results;
using MediatR;

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
            MMM mmmDocument;
            try
            {
                mmmDocument = await _mmmRepository.GetDocumentByIdWithItemsAsync(request.DocumentId);
                //.Include(x => x.DocumentItems)
                //  .ThenInclude(x => x.DocumentWarehouseUnitItems)
                //      .ThenInclude(x => x.WarehouseUnitItem)
            }
            catch (KeyNotFoundException ex)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The Document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<MmmDto>(vr);
            }
            catch (Exception ex)
            {
                throw;
            }

            mmmDocument.IsApproved = true;
            mmmDocument.ApprovalDate = DateTime.Now;
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

            foreach (var di in mmmDocument.DocumentItems)
            {
                var newDocumentItem = new DocumentItem
                {
                    DocumentItemId = Guid.NewGuid().ToString(),
                    DocumentId = entityMmp.DocumentId,
                    ProductId = di.ProductId,
                    ProductCode = di.ProductCode,
                    EanCode = di.EanCode,
                    ProductName = di.ProductName,
                    Quantity = di.Quantity,
                    BestBefore = di.BestBefore,
                    BatchLot = di.BatchLot,
                    SerialNumber = di.SerialNumber,
                    Price = di.Price,
                    Currency = di.Currency,
                    IsApproved = di.IsApproved,
                    CreatedAt = DateTime.Now,
                    CreatedBy = di.CreatedBy,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = di.ModifiedBy,
                    Document = null,
                    Product = null,
                    DocumentWarehouseUnitItems = []
                };

                foreach (var dwui in di.DocumentWarehouseUnitItems)
                {
                    newDocumentItem.DocumentWarehouseUnitItems.Add(new DocumentWarehouseUnitItem
                    {
                        DocumentWarehouseUnitItemId = Guid.NewGuid().ToString(),
                        DocumentItemId = newDocumentItem.DocumentItemId,
                        WarehouseUnitItemId = dwui.WarehouseUnitItemId,
                        Quantity = dwui.Quantity,
                        CreatedAt = DateTime.Now
                    });
                }

                entityMmp.DocumentItems.Add(newDocumentItem);
            }

            //var warehouseUnitsResponse = await _mediator.Send(
            //    new GetSortedFilteredWarehouseUnitsQuery(
            //        new SieveModel()
            //        {
            //            Page = 1,
            //            PageSize = 1000,
            //            Filters = "FilterByWarehouseUnitItemIds==" + string.Join('|', mmmDocument.DocumentItems.SelectMany(di => di.DocumentWarehouseUnitItems).Select(wui => wui.WarehouseUnitItemId).Distinct())
            //        }));

            //List<WarehouseUnitDto> warehouseUnitsToMoveDto = warehouseUnitsResponse.ReturnedObj?.Items.ToList() ?? [];
            List<WarehouseUnit> warehouseUnitsToMove;
            try
            {
                //warehouseUnitsToMove = _mapper.Map<List<WarehouseUnit>>(warehouseUnitsToMoveDto) ?? [];
                warehouseUnitsToMove = mmmDocument
                    .DocumentItems
                    .SelectMany(di => di.DocumentWarehouseUnitItems)
                    .Select(dwui => dwui.WarehouseUnitItem)
                    .Select(wui => wui!.WarehouseUnit).ToList()!;

            }
            catch (Exception ex)
            {

                throw;
            }

            if (/*!warehouseUnitsResponse.Success ||*/ !warehouseUnitsToMove.Any())
            {
                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
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

                var movedWarehouseUnits = await _warehouseUnitRepository
                    .UpdateWarehouseUnitsAsync(warehouseUnitsToMove.ToArray());

                await _transactionManager.CommitTransactionAsync();

                updatedMmmDto = _mapper.Map<MmmDto>(updatedMmm);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmmDto>(updatedMmmDto);
        }
    }
}
