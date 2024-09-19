using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePwItems
{
    internal class ApprovePwItemsCommandHandler
        (IPwRepository pwRepozitory,
        IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApprovePwItemsCommand, BaseResponse<PwDto>>
    {
        private readonly IPwRepository _pwRepozitory = pwRepozitory;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<PwDto>> Handle
            (ApprovePwItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApprovePwItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PwDto>(validationResult);
            }

            var document = await _pwRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);
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

            document.ApproveDocumentItems(request.ModifiedBy);

            try
            {
                return await DocumentWarehouseTransactionService.CommitChangesAsync<PW, PwDto>
                    (document, warehouseUnits, _transactionManager, _warehouseUnitRepository, _pwRepozitory, _mapper);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();
                return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
