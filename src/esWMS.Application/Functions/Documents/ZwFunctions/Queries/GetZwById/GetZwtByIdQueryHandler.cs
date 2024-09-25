using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetZwById
{
    internal class GetZwByIdQueryHandler
        (IBaseDocumentRepository<ZW> repository, IMapper mapper)
        : IRequestHandler<GetZwByIdQuery, BaseResponse<ZwDto>>
    {
        private readonly IBaseDocumentRepository<ZW> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ZwDto>> Handle
            (GetZwByIdQuery request, CancellationToken cancellationToken)
        {
            ZwDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.PwId);

                if (document == null)
                {
                    return new BaseResponse<ZwDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<ZwDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ZwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<ZwDto>(baseDocumentDto);
        }
    }
}
