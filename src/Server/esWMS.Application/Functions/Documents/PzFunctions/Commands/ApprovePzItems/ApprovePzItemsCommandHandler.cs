using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnvironment;
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
            var validationResult = await new ApprovePzItemsValidator(_mediator)
                .ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PzDto>(validationResult);
            }

            var document = await _pzRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
            var warehouseUnits = await _warehouseUnitRepository.GetWarehouseUnitsWithItemsByIdsAsync(
                request.DocumentItemsWithAssignment
                .Select(x => x.WarehouseUnitId)
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

            document.ApproveDocumentItems(request.ModifiedBy);

            try
            {
                //return await CommitChangesAsync(document, warehouseUnits);
                return await DocumentWarehouseTransactionService.CommitChangesAsync<PZ, PzDto>
                    (document, warehouseUnits, _transactionManager, _warehouseUnitRepository, _pzRepozitory, _mapper);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();
                return new BaseResponse<PzDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }

        private async Task<BaseResponse<PzDto>> CommitChangesAsync
            (PZ document, IEnumerable<WarehouseUnit> warehouseUnits)
        {
            await _transactionManager.BeginTransactionAsync();

            var updatedWarehouseUnits = await _warehouseUnitRepository.UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());
            var updatedDocument = await _pzRepozitory.UpdateAsync(document);

            await _transactionManager.CommitTransactionAsync();

            var mappedUpdatedDocument = _mapper.Map<PzDto>(updatedDocument);
            return new BaseResponse<PzDto>(mappedUpdatedDocument);
        }
    }
}
