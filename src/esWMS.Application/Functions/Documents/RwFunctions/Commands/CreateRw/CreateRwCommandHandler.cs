using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.CreateRw
{
    internal class CreateRwCommandHandler
        (IRwRepository rwRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreateRwCommand, BaseResponse<RwDto>>
    {
        private readonly IRwRepository _rwRepository = rwRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<RwDto>> Handle(CreateRwCommand request, CancellationToken cancellationToken)
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

            if (!productResponse.IsSuccess() || products.Count == 0)
            {
                return new BaseResponse<RwDto>(productResponse.Status, "Something went wrong. An error occurred while retrieving the list of products associated with the document.");
            }

            var validationResult = await new CreateRwValidator(products, _mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<RwDto>(validationResult);
            }

            var entity = _mapper.Map<RW>(request);

            if (entity == null)
            {
                return new BaseResponse<RwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var lastNumber = await _rwRepository.GetAllDocumentIdForDay(entity.DocumentIssueDate);

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
                    warehouseUnitItemsQuantityToBlock
                        .Add(documentItemWithAssignment.WarehouseUnitItemId!, documentItemWithAssignment.Quantity);
                }
            }

            RwDto entityDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var createdEntity = await _rwRepository.CreateAsync(entity);

                var warehouseUnitItems = await _warehouseUnitItemRepository
                      .BlockExistWarehouseUnitItemsQuantityAsync(warehouseUnitItemsQuantityToBlock);

                await _transactionManager.CommitTransactionAsync();

                entityDto = _mapper.Map<RwDto>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<RwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<RwDto>(entityDto);
        }
    }
}
