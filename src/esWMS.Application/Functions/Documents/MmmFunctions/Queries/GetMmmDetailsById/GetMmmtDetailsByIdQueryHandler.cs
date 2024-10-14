using AutoMapper;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetMmmDetailsById
{
    internal class GetMmmDetailsByIdQueryHandler
        (IMmmRepository repository, IMapper mapper)
        : IRequestHandler<GetMmmDetailsByIdQuery, BaseResponse<MmmDetailsDto>>
    {
        private readonly IMmmRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<MmmDetailsDto>> Handle(GetMmmDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            MmmDetailsDto documentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (document == null)
                {
                    return new BaseResponse<MmmDetailsDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                documentDto = _mapper.Map<MmmDetailsDto>(document);

                documentDto.RelatedWarehouseUnitIds = document.DocumentItems
                            .SelectMany(x => x.DocumentWarehouseUnitItems)
                            .Select(x => x.WarehouseUnitItem!.WarehouseUnitId)
                            .Distinct()
                            .ToList();
            }
            catch (Exception ex)
            {
                return new BaseResponse<MmmDetailsDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmmDetailsDto>(documentDto);
        }
    }
}
