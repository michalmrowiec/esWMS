using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Queries.GetContractorById
{
    public record GetContractorByIdQuery(string ContractorId)
        : IRequest<BaseResponse<ContractorDto>>;
}
