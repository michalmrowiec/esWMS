using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Commands.DeleteContractor
{
    public record DeleteContractorCommand(string ContractorId) : IRequest<BaseResponse>;
}
