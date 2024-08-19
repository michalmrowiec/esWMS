using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz
{
    internal class CreateWzCommandHandler
        (IWzRepository wzRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreateWzCommand, BaseResponse<WzDto>>
    {
        private readonly IWzRepository _wzRepository = wzRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<WzDto>> Handle(CreateWzCommand request, CancellationToken cancellationToken)
        {
            var productResponse = await _mediator.Send(
                new GetSortedFilteredProductsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 500,
                        Filters = "ProductId==" + string.Join('|', request.DocumentItems.Select(x => x.ProductId).Distinct())
                    }));

            var products = productResponse.ReturnedObj?.Items ?? [];

            if (!productResponse.Success)
            {
                return new BaseResponse<WzDto>(false, "Something went wrong.");
            }

            var validationResult = await new CreateWzValidator(products, _mediator).ValidateAsync(request, cancellationToken);

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
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = request.CreatedBy;

            foreach (var item in entity.DocumentItems)
            {
                item.DocumentItemId = Guid.NewGuid().ToString();
                item.DocumentId = entity.DocumentId;
                item.IsApproved = false;

                var product = products!.First(x => x.ProductId.Equals(item.ProductId));
                item.ProductCode = product.ProductCode;
                item.EanCode = product.EanCode;
                item.ProductName = product.ProductName;

                foreach (var itemAssignment in item.DocumentWarehouseUnitItems)
                {
                    itemAssignment.DocumentItemId = item.DocumentItemId;
                    itemAssignment.WarehouseUnitItemId = itemAssignment.WarehouseUnitItemId!;
                    itemAssignment.Quantity = itemAssignment.Quantity;
                    itemAssignment.CreatedAt = DateTime.Now;
                    itemAssignment.CreatedBy = request.CreatedBy;
                }
            }

            Dictionary<string, int> warehouseUnitItemsQuantityToBlock = new();

            foreach (var documentItem in request.DocumentItems)
            {
                foreach (var documentItemWithAssignment in documentItem.DocumentWarehouseUnitItems)
                {
                    // TODO check the blocked before add document!!!
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
