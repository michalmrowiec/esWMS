using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById
{
    internal class GetWzByIdQueryHandler
        (IBaseDocumentRepository<WZ> repository, IMapper mapper)
        : IRequestHandler<GetWzByIdQuery, BaseResponse<WzDto>>
    {
        private readonly IBaseDocumentRepository<WZ> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<WzDto>> Handle(GetWzByIdQuery request, CancellationToken cancellationToken)
        {
            WzDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.WzId);

                if (document == null)
                {
                    return new BaseResponse<WzDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<WzDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<WzDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<WzDto>(baseDocumentDto);
        }
    }
}
