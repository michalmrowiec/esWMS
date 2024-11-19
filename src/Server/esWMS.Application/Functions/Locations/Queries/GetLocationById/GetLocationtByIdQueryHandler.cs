using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Locations.Queries.GetLocationById
{
    internal class GetLocationByIdQueryHandler
        (ILocationRepository repository, IMapper mapper)
        : IRequestHandler<GetLocationByIdQuery, BaseResponse<LocationDto>>
    {
        private readonly ILocationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<LocationDto>> Handle
            (GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            LocationDto locationDto;

            try
            {
                var document = await _repository.GetByIdAsync(request.LocationId);

                if (document == null)
                {
                    return new BaseResponse<LocationDto>
                        (BaseResponse.ResponseStatus.NotFound, "The document does not exist.");
                }

                locationDto = _mapper.Map<LocationDto>(document);
            } //KeyNotFoundException?? TODO
            catch (Exception ex)
            {
                return new BaseResponse<LocationDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<LocationDto>(locationDto);
        }
    }
}
