using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.WarehouseEnviroment;
using MediatR;

namespace esWMS.Application.Functions.Locations.Commands.DeleteLocation
{
    internal class DeleteLocationCommandHandler(
        ILocationRepository repository)
        : IRequestHandler<DeleteLocationCommand, BaseResponse>
    {
        private readonly ILocationRepository _repository = repository;

        public async Task<BaseResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            Location? location;
            try
            {
                location = await _repository.GetByIdAsync(request.LocationId);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.NotFound, "Location not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var validationResult = await new DeleteLocationValidator
                (location).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse(validationResult);
            }

            try
            {
                await _repository.DeleteAsync(location.LocationId);
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
