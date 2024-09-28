using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Commands.CreateContractor
{
    public class CreateContractorCommand : CommonContractorCommand, IRequest<BaseResponse<ContractorDto>>
    {
        public string? CreatedBy { get; set; }
    }
}
