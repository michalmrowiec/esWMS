using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functiosns.Documents.PwFunctions.Queries.GetPwById;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Queries.GetPwById
{
    internal class GetPwByIdQueryHandler
        (IBaseDocumentRepository<PW> repository, IMapper mapper)
        : IRequestHandler<GetPwByIdQuery, BaseResponse<PwDto>>
    {
        private readonly IBaseDocumentRepository<PW> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PwDto>> Handle
            (GetPwByIdQuery request, CancellationToken cancellationToken)
        {
            PwDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.PwId);

                if (document == null)
                {
                    return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<PwDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<PwDto>(baseDocumentDto);
        }
    }
}
