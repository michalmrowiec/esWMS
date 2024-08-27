using esMWS.Domain.Models;
using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Contractors.Queries.GetSortedFilteredContractors;
using esWMS.Application.Responses;
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
        public async Task<ActionResult<PagedResult<ContractorDto>>>GetSortedAndFilteredCategories([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredContractorQuery(sieveModel));

            switch (result.Status)
            {
                case BaseResponse.ResponseStatus.Success:
                    return Ok(result.ReturnedObj);
                case BaseResponse.ResponseStatus.ValidationError:
                    return BadRequest(result.ValidationErrors);
                case BaseResponse.ResponseStatus.ServerError:
                    return StatusCode(500);
                case BaseResponse.ResponseStatus.NotFound:
                    return NotFound();
                case BaseResponse.ResponseStatus.BadQuery:
                    return BadRequest(result.Message);
                default:
                    return BadRequest();
            }
        }
    }
}
