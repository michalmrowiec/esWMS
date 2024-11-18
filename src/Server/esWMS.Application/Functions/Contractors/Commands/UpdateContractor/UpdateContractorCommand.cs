using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Commands.UpdateContractor
{
    public class UpdateContractorCommand : CommonContractorCommand, IRequest<BaseResponse<ContractorDto>>
    {
        public string? ModifiedBy { get; set; }
    }
}
