using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById
{
    internal class GetDocumentByIdQueryHandler
        (IBaseDocumentRepository<BaseDocument> repository, IMapper mapper)
        : IRequestHandler<GetDocumentByIdQuery, BaseResponse<BaseDocumentDto>>
    {
        private readonly IBaseDocumentRepository<BaseDocument> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<BaseDocumentDto>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            BaseDocumentDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (document == null)
                {
                    return new BaseResponse<BaseDocumentDto>(false, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<BaseDocumentDto>(document);
            }
            catch (Exception)
            {
                return new BaseResponse<BaseDocumentDto>(false, "Something went wrong.");
            }

            return new BaseResponse<BaseDocumentDto>(baseDocumentDto);
        }
    }
}
