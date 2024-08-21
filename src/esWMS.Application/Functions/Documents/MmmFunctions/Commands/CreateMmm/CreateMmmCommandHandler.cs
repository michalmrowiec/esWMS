using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz;
using esWMS.Application.Functions.Documents.WzFunctions;
using esWMS.Application.Responses;
using MediatR;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using Sieve.Models;
using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm
{
    internal class CreateMmmCommandHandler
        (IMmmRepository mmmRepository,
        IMmpRepository mmpRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreateMmmCommand, BaseResponse<MmmDto>>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IMmpRepository _mmpRepository = mmpRepository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<MmmDto>> Handle(CreateMmmCommand request, CancellationToken cancellationToken)
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

            Dictionary<string, int> warehouseUnitItemsQuantityToBlock = new();

            var lastNumberMmm = await _mmmRepository.GetAllDocumentIdForDay(entityMmm.DocumentIssueDate);
            var lastNumberMmp = await _mmpRepository.GetAllDocumentIdForDay(entityMmm.DocumentIssueDate);

            entityMmm.DocumentId = entityMmm.GenerateDocumentId(lastNumberMmm);
            entityMmm.CreatedAt = DateTime.Now;
            entityMmm.CreatedBy = request.CreatedBy;

            var entityMmp = new MMP
                ("",
                issueWarehouseId: entityMmm.ToWarehouseId,
                comment: entityMmm.Comment,
                documentIssueDate: entityMmm.DocumentIssueDate,
                issuingEmployeeId: null,
                assignedEmployeeId: null,
                isApproved: false,
                aprovedDate: null,
                approvingEmployeeId: null,
                goodsReceiptDate: null,
                fromWarehouseId: entityMmm.IssueWarehouseId,
                relatedMmmId: entityMmm.DocumentId,
                createdAt: entityMmm.CreatedAt,
                createdBy: null,
                modifiedAt: null,
                modifiedBy: null);

            entityMmp.DocumentId = entityMmp.GenerateDocumentId(lastNumberMmp);

            foreach (var item in entityMmm.DocumentItems)
            {
                item.DocumentItemId = Guid.NewGuid().ToString();
                item.DocumentId = entityMmm.DocumentId;

                if (request.DocumentItemsAreApproved)
                {
                    item.IsApproved = request.DocumentItemsAreApproved;
                }
                else
                {
                    item.IsApproved = item.IsApproved;
                }

                var product = products!.First(x => x.ProductId.Equals(item.ProductId));
                item.ProductCode = product.ProductCode;
                item.EanCode = product.EanCode;
                item.ProductName = product.ProductName;

                foreach (var itemAssignment in item.DocumentWarehouseUnitItems)
                {
                    itemAssignment.DocumentItemId = item.DocumentItemId;
                    //itemAssignment.WarehouseUnitItemId = itemAssignment.WarehouseUnitItemId!;
                    itemAssignment.Quantity = itemAssignment.Quantity;
                    itemAssignment.CreatedAt = DateTime.Now;
                    itemAssignment.CreatedBy = request.CreatedBy;

                    warehouseUnitItemsQuantityToBlock
                        .Add(itemAssignment.WarehouseUnitItemId!, itemAssignment.Quantity);
                }
            }

            entityMmp.DocumentItems = entityMmm.DocumentItems; // ???

            var listOfNewWarehouseUnitItemsForTargetWarehouse = new List<WarehouseUnitItem>();

            foreach (var documentItem in entityMmp.DocumentItems)
            {
                if (request.DocumentItemsAreApproved)
                {
                    documentItem.IsApproved = request.DocumentItemsAreApproved;
                }
                else
                {
                    documentItem.IsApproved = false;
                }

                foreach (var unitItems in documentItem.DocumentWarehouseUnitItems)
                {
                    //listOfNewWarehouseUnitItemsForTargetWarehouse
                        //.Add(new())
                }
            }

            MmmDto entityDto;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var createdEntity = await _mmmRepository.CreateAsync(entityMmm);

                var warehouseUnitItems = await _warehouseUnitItemRepository
                      .BlockExistWarehouseUnitItemsQuantityAsync(warehouseUnitItemsQuantityToBlock);



                await _transactionManager.CommitTransactionAsync();

                entityDto = _mapper.Map<MmmDto>(createdEntity);
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
