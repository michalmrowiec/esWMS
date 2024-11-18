using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Queries.GetRwById
{
    internal class GetRwByIdQueryHandler
        (IBaseDocumentRepository<RW> repository, IMapper mapper)
        : IRequestHandler<GetRwByIdQuery, BaseResponse<RwDto>>
    {
        private readonly IBaseDocumentRepository<RW> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<RwDto>> Handle(GetRwByIdQuery request, CancellationToken cancellationToken)
        {
            RwDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.WzId);

                if (document == null)
                {
                    return new BaseResponse<RwDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<RwDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<RwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<RwDto>(baseDocumentDto);
        }
    }
}
