using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetListOfWarehouseUnitsInMMP
{
    internal class GetListOfWarehouseUnitIdsRelatedMMPQueryHandler
        (IBaseDocumentRepository<MMP> repository,
        IMapper mapper)
        : IRequestHandler<GetListOfWarehouseUnitIdsRelatedMMPQuery, BaseResponse<string[]>>
    {
        private readonly IBaseDocumentRepository<MMP> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<string[]>> Handle
            (GetListOfWarehouseUnitIdsRelatedMMPQuery request, CancellationToken cancellationToken)
        {
            string[] warehouseUnitIds;
            try
            {
                var result = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (result == null)
                {
                    return new BaseResponse<string[]>(BaseResponse.ResponseStatus.NotFound, "Document not found.");
                }

                if (result.IsApproved)
                {
                    return new BaseResponse<string[]>(BaseResponse.ResponseStatus.BadQuery, "Document is approved.");
                }

                string?[] ids = result.DocumentItems
                                    .SelectMany(x => x.DocumentWarehouseUnitItems)
                                    .Select(x => x.WarehouseUnitItem?.WarehouseUnitId)
                                    .Distinct()
                                    .ToArray();

                if (ids is null
                    || ids.Any(string.IsNullOrEmpty))
                {
                    throw new KeyNotFoundException("The object with the given id was not found.");
                }

                warehouseUnitIds = ids!;
            }
            catch (Exception ex)
            {
                return new BaseResponse<string[]>(BaseResponse.ResponseStatus.NotFound, "Something went wrong.");
            }

            return new BaseResponse<string[]>(warehouseUnitIds);
        }
    }
}
