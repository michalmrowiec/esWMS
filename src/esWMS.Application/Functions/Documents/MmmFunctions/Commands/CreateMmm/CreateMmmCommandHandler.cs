using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm
{
    internal class CreateMmmCommandHandler
        (IMmmRepository mmmRepository,
        IWarehouseUnitRepository warehouseUnitRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreateMmmCommand, BaseResponse<MmmDto>>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<MmmDto>> Handle
            (CreateMmmCommand request, CancellationToken cancellationToken)
        {
            var productResponse = await _mediator.Send(
                new GetSortedFilteredProductsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 1000,
                        Filters = "ProductId==" + string.Join('|', request.WarehouseUnits.SelectMany(wu => wu.WarehouseUnitItems).Select(wui => wui.ProductId).Distinct())
                    }));

            var products = _mapper.Map<List<Product>>(productResponse.ReturnedObj?.Items) ?? [];

            if (!productResponse.Success)
            {
                return new BaseResponse<MmmDto>(false, "Something went wrong.");
            }

            var validationResult = await new CreateMmmValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<MmmDto>(validationResult);
            }

            var entityMmm = _mapper.Map<MMM>(request);

            if (entityMmm == null)
            {
                return new BaseResponse<MmmDto>(false, "Something went wrong.");
            }

            var lastNumberMmm = await _mmmRepository.GetAllDocumentIdForDay(entityMmm.DocumentIssueDate);

            entityMmm.DocumentId = entityMmm.GenerateDocumentId(lastNumberMmm);
            entityMmm.CreatedAt = DateTime.Now;
            entityMmm.CreatedBy = request.CreatedBy;

            Dictionary<string, int> warehouseUnitItemsQuantityToBlockSubstract =
                request.WarehouseUnits
                .SelectMany(wu => wu.WarehouseUnitItems)
                .ToDictionary(key => key.WarehouseUnitItemId, item => item.Quantity);

            foreach (var wui in request.WarehouseUnits.SelectMany(wu => wu.WarehouseUnitItems))
            {
                wui.Product = products.First(p => p.ProductId.Equals(wui.ProductId));

                var documentItem = _mapper.Map<DocumentItem>(wui);
                documentItem.DocumentItemId = Guid.NewGuid().ToString();
                documentItem.CreatedAt = DateTime.Now;
                documentItem.IsApproved = true;

                documentItem.DocumentWarehouseUnitItems = request.WarehouseUnits
                    .Select(wu => new DocumentWarehouseUnitItem()
                    {
                        DocumentItemId = documentItem.DocumentItemId,
                        WarehouseUnitItemId = wui.WarehouseUnitItemId,
                        Quantity = wui.Quantity,
                        CreatedAt = DateTime.Now
                    }).ToList();

                entityMmm.DocumentItems.Add(documentItem);
            }

            MmmDto entityDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var createdMmm = await _mmmRepository.CreateAsync(entityMmm);

                await _transactionManager.CommitTransactionAsync();

                entityDto = _mapper.Map<MmmDto>(createdMmm);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<MmmDto>(false, "Something went wrong.");
            }

            return new BaseResponse<MmmDto>(entityDto);
        }
    }
}
