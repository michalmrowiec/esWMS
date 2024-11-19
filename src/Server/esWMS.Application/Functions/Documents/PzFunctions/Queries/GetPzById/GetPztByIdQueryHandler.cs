using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById
{
    internal class GetPzByIdQueryHandler
        (IBaseDocumentRepository<PZ> repository, IMapper mapper)
        : IRequestHandler<GetPzByIdQuery, BaseResponse<PzDto>>
    {
        private readonly IBaseDocumentRepository<PZ> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PzDto>> Handle(GetPzByIdQuery request, CancellationToken cancellationToken)
        {
            PzDto baseDocumentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.PzId);

                if (document == null)
                {
                    return new BaseResponse<PzDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                baseDocumentDto = _mapper.Map<PzDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PzDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<PzDto>(baseDocumentDto);
        }
    }
}
