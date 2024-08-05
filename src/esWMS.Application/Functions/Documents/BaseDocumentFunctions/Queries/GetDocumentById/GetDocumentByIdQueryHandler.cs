using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById
{
    internal class GetDocumentByIdQueryHandler<TDocument, TDocumentDto>
        (IBaseDocumentRepository<TDocument> repository, IMapper mapper)
        : IRequestHandler<GetDocumentByIdQuery<TDocument, TDocumentDto>, BaseResponse<TDocumentDto>>
        where TDocument : BaseDocument
        where TDocumentDto : BaseDocumentDto
    {
        private readonly IBaseDocumentRepository<TDocument> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<TDocumentDto>> Handle(GetDocumentByIdQuery<TDocument, TDocumentDto> request, CancellationToken cancellationToken)
        {
            TDocumentDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (document == null)
                {
                    return new BaseResponse<TDocumentDto>(false, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<TDocumentDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<TDocumentDto>(false, "Something went wrong.");
            }

            return new BaseResponse<TDocumentDto>(baseDocumentDto);
        }
    }
}
