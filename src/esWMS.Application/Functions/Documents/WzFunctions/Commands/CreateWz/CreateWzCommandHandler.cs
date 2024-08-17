using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz
{
    internal class CreateWzCommandHandler
        (IWzRepository wzRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreateWzCommand, BaseResponse<WzDto>>
    {
        private readonly IWzRepository _wzRepository = wzRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;

        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<WzDto>> Handle(CreateWzCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateWzValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WzDto>(validationResult);
            }

            var entity = _mapper.Map<WZ>(request);

            if (entity == null)
            {
                return new BaseResponse<WzDto>(false, "");
            }

            var lastNumber = await _wzRepository.GetAllDocumentIdForDay(entity.DocumentIssueDate);

            entity.DocumentId = entity.GenerateDocumentId(lastNumber);

            foreach (var item in entity.DocumentItems)
            {
                item.DocumentItemId = Guid.NewGuid().ToString();
                item.DocumentId = entity.DocumentId;
                item.IsApproved = false;

                foreach (var itemAssignment in
                    request.DocumentItems.First(x => x.ProductId.Equals(item.ProductId)).DocumentItemsWithAssignment)
                {
                    var newDocumentWarehouseUnitItem = new DocumentWarehouseUnitItem
                    {
                        DocumentItemId = item.DocumentItemId,
                        WarehouseUnitItemId = itemAssignment.WarehouseUnitItemId!,
                        Quantity = itemAssignment.Quantity,
                        CreatedAt = DateTime.Now,
                        CreatedBy = request.CreatedBy
                    };

                    item.DocumentWarehouseUnitItems.Add(newDocumentWarehouseUnitItem);
                }
            }

            Dictionary<string, int> warehouseUnitItemsQuantityToBlock = new();

            foreach (var documentItem in request.DocumentItems)
            {
                foreach (var documentItemWithAssignment in documentItem.DocumentItemsWithAssignment)
                {
                    warehouseUnitItemsQuantityToBlock
                        .Add(documentItemWithAssignment.WarehouseUnitItemId!, documentItemWithAssignment.Quantity);
                }
            }

            WzDto entityDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var createdEntity = await _wzRepository.CreateAsync(entity);

                var warehouseUnitItems = await _warehouseUnitItemRepository
                      .BlockWarehouseUnitItemsQuantityAsync(warehouseUnitItemsQuantityToBlock);

                await _transactionManager.CommitTransactionAsync();

                entityDto = _mapper.Map<WzDto>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<WzDto>(false, "Something went wrong.");
            }

            return new BaseResponse<WzDto>(entityDto);
        }
    }
}
