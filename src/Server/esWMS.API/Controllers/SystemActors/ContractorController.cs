using esWMS.API.Controllers.Utils;
using esWMS.API.Services;
using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Contractors.Commands.CreateContractor;
using esWMS.Application.Functions.Contractors.Commands.DeleteContractor;
using esWMS.Application.Functions.Contractors.Commands.UpdateContractor;
using esWMS.Application.Functions.Contractors.Queries.GetContractorById;
using esWMS.Application.Functions.Contractors.Queries.GetSortedFilteredContractors;
using esWMS.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.API.Controllers.SystemActors
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContractorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContractorController> _logger;
        private readonly IUserContextService _userContextService;

        public ContractorController(
            ILogger<ContractorController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<ContractorDto>>> GetSortedAndFilteredContractors
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredContractorQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpPost]
        public async Task<ActionResult<ContractorDto>> CreateContractor
            ([FromBody] CreateContractorCommand createContractorCommand)
        {
            if (_userContextService.GetUserId is not null)
                createContractorCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createContractorCommand);

            return result.HandleCreatedResult(this, "");
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpDelete("{contractorId}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] string contractorId)
        {
            var result = await _mediator.Send(new DeleteContractorCommand(contractorId));

            return result.HandleNoContentResult(this);
        }

        [HttpGet]
        public async Task<ActionResult<ContractorDto>> GetContractorDetails
            ([FromQuery] string contractorId)
        {
            var result = await _mediator.Send(new GetContractorByIdQuery(contractorId));

            return result.HandleOkResult(this);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpPut]
        public async Task<ActionResult<ContractorDto>> UpdateContractor
            ([FromBody] UpdateContractorCommand updateContractorCommand)
        {
            if (_userContextService.GetUserId is not null)
                updateContractorCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(updateContractorCommand);

            return result.HandleOkResult(this);
        }
    }
}
