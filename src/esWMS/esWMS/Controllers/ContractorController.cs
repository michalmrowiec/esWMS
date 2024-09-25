using esWMS.Domain.Models;
using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Contractors.Commands.CreateContractor;
using esWMS.Application.Functions.Contractors.Queries.GetSortedFilteredContractors;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
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
        public async Task<ActionResult<PagedResult<ContractorDto>>> GetSortedAndFilteredCategories([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredContractorQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<ContractorDto>> CreateContractor
            ([FromBody] CreateContractorCommand createContractorCommand)
        {
            if (_userContextService.GetUserId is not null)
                createContractorCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createContractorCommand);

            return result.HandleCreatedResult(this, "");
        }
    }
}
