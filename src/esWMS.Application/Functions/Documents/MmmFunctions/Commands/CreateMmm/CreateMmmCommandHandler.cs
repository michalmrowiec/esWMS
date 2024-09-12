using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Products;
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
            List<WarehouseUnit> warehouseUnitsToMove = [];
            BaseResponse<PagedResult<ProductDto>>? productResponse = null;
            List<ProductDto> products = [];

            try
            {
                foreach (var wuId in request.WarehouseUnitIds.Distinct())
                {
                    var warehouseUnit = await _warehouseUnitRepository.GetFullWarehouseUnitStackAsync(wuId);

                    if (warehouseUnit == null)
                    {
                        return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.NotFound, "Warehouse unit not found.");
                    }

                    warehouseUnitsToMove.AddRange(warehouseUnit);
                }

                warehouseUnitsToMove = warehouseUnitsToMove.Distinct().ToList();

                productResponse = await _mediator.Send(
                    new GetSortedFilteredProductsQuery(
                        new SieveModel()
                        {
                            Page = 1,
                            PageSize = 1000,
                            Filters = "ProductId==" + string.Join('|', warehouseUnitsToMove.SelectMany(wu => wu.WarehouseUnitItems).Select(wui => wui.ProductId).Distinct())
                        }));

                products = productResponse.ReturnedObj?.Items.ToList() ?? [];
            }
            catch (Exception)
            {
                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
                throw;
            }

            var validationResult = await new CreateMmmValidator(warehouseUnitsToMove).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<MmmDto>(validationResult);
            }

            if (!productResponse.IsSuccess() || products.Count == 0)
            {
                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong. An error occurred while retrieving the list of products associated with the document.");
            }

            var entityMmm = _mapper.Map<MMM>(request);

            if (entityMmm == null)
            {
                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var lastNumberMmm = await _mmmRepository.GetAllDocumentIdForDay(entityMmm.DocumentIssueDate);

            entityMmm.DocumentId = entityMmm.GenerateDocumentId(lastNumberMmm);
            entityMmm.CreatedAt = DateTime.Now;
            entityMmm.CreatedBy = request.CreatedBy;

            foreach (var wu in warehouseUnitsToMove)
            {
                foreach (var wui in wu.WarehouseUnitItems)
                {
                    var prod = products.First(p => p.ProductId.Equals(wui.ProductId));

                    var documentItem = new DocumentItem()
                    {
                        ProductId = prod.ProductId,
                        ProductCode = prod.ProductCode,
                        EanCode = prod.EanCode,
                        ProductName = prod.ProductName,
                        Quantity = wui.Quantity,
                        BestBefore = wui.BestBefore,
                        BatchLot = wui.BatchLot,
                        SerialNumber = wui.SerialNumber,
                        Price = wui.Price,
                        Currency = wui.Currency,
                        DocumentItemId = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.Now,
                        IsApproved = true,
                        Document = null,
                        DocumentWarehouseUnitItems = []
                    };

                    documentItem.DocumentWarehouseUnitItems.Add(new DocumentWarehouseUnitItem()
                    {
                        DocumentItemId = documentItem.DocumentItemId,
                        WarehouseUnitItemId = wui.WarehouseUnitItemId,
                        Quantity = wui.Quantity,
                        CreatedAt = DateTime.Now,

                        DocumentItem = null,
                        WarehouseUnitItem = null
                    });

                    entityMmm.DocumentItems.Add(documentItem);
                }
            }

            foreach (var di in entityMmm.DocumentItems)
            {
                di.Document = null;
            }

            MmmDto entityDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var createdMmm = await _mmmRepository.CreateAsync(entityMmm);

                var warehouseUnits = await _warehouseUnitRepository
                    .SetWarehouseUnitsBlockedStatusAsync(true, warehouseUnitsToMove.Select(wu => wu.WarehouseUnitId).ToArray());

                await _transactionManager.CommitTransactionAsync();

                entityDto = _mapper.Map<MmmDto>(createdMmm);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmmDto>(entityDto);
        }
    }
}
