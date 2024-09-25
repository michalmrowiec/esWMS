using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById
{
    internal class GetMmpByIdQueryHandler
        (IBaseDocumentRepository<MMP> repository, IMapper mapper)
        : IRequestHandler<GetMmpByIdQuery, BaseResponse<MmpDto>>
    {
        private readonly IBaseDocumentRepository<MMP> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<MmpDto>> Handle(GetMmpByIdQuery request, CancellationToken cancellationToken)
        {
            MmpDto documentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (document == null)
                {
                    return new BaseResponse<MmpDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                documentDto = _mapper.Map<MmpDto>(document);
            }
            catch (Exception ex)
            {
                return new BaseResponse<MmpDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmpDto>(documentDto);
        }
    }
}
