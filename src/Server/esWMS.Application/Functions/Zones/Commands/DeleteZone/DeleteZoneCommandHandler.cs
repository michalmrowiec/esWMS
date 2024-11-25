using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Locations.Queries.GetSortedFilteredLocations;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.WarehouseEnvironment;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Zones.Commands.DeleteZone
{
    internal class DeleteZoneCommandHandler(
        IZoneRepository repository,
        IMediator mediator)
        : IRequestHandler<DeleteZoneCommand, BaseResponse>
    {
        private readonly IZoneRepository _repository = repository;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
        {
            Zone? zone;
            try
            {
                zone = await _repository.GetByIdAsync(request.ZoneId);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.NotFound, "Zone not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var ocationResponse = await _mediator.Send(
                new GetSortedFilteredLocationsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 500,
                        Filters = "ZoneId==" + request.ZoneId
                    }));

            var locationsInZone = ocationResponse.ReturnedObj?.Items ?? [];

            if (!ocationResponse.IsSuccess())
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var validationResult = await new DeleteZoneValidator
                (locationsInZone).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse(validationResult);
            }

            try
            {
                await _repository.DeleteAsync(zone.ZoneId);
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
