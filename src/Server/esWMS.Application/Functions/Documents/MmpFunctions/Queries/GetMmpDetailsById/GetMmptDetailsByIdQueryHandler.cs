using AutoMapper;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetMmpDetailsById
{
    internal class GetMmpDetailsByIdQueryHandler
        (IMmpRepository repository, IMapper mapper)
        : IRequestHandler<GetMmpDetailsByIdQuery, BaseResponse<MmpDetailsDto>>
    {
        private readonly IMmpRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<MmpDetailsDto>> Handle(GetMmpDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            MmpDetailsDto documentDto;

            try
            {
                var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                if (document == null)
                {
                    return new BaseResponse<MmpDetailsDto>(BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                documentDto = _mapper.Map<MmpDetailsDto>(document);

                //documentDto.RelatedWarehouseUnitIds = document.DocumentItems
                //            .SelectMany(x => x.DocumentWarehouseUnitItems)
                //            .Select(x => x.WarehouseUnitItem!.WarehouseUnitId)
                //            .Distinct()
                //            .ToList();
            }
            catch (Exception ex)
            {
                return new BaseResponse<MmpDetailsDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<MmpDetailsDto>(documentDto);
        }
    }
}
