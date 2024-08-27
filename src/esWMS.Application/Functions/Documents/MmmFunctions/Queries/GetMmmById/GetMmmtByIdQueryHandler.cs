using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById
{
    internal class GetMmmByIdQueryHandler
        (IBaseDocumentRepository<MMM> repository, IMapper mapper)
        : IRequestHandler<GetMmmByIdQuery, BaseResponse<MmmDto>>
    {
        private readonly IBaseDocumentRepository<MMM> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<MmmDto>> Handle(GetMmmByIdQuery request, CancellationToken cancellationToken)
        {
            MmmDto documentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (document == null)
                {
                    return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                documentDto = _mapper.Map<MmmDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<MmmDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmmDto>(documentDto);
        }
    }
}
