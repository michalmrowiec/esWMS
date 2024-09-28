using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.SystemActors;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Commands.DeleteContractor
{
    internal class DeleteContractorCommandHandler
        (IContractorRepository repository)
        : IRequestHandler<DeleteContractorCommand, BaseResponse>
    {
        private readonly IContractorRepository _repository = repository;

        public async Task<BaseResponse> Handle(DeleteContractorCommand request, CancellationToken cancellationToken)
        {
            Contractor? contractor;
            try
            {
                contractor = await _repository.GetByIdAsync(request.ContractorId);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.NotFound, "Contractor not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            if (contractor.WZDocuments.Any() || contractor.PZDocuments.Any())
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Can not delete a contractor that is assigned to any document.");
            }

            try
            {
                await _repository.DeleteAsync(contractor.ContractorId);
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
