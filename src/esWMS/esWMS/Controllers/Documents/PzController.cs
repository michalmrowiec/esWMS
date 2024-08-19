using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePz;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PzController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PzController> _logger;
        private readonly IUserContextService _userContextService;

        public PzController(
            ILogger<PzController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<PzDto>> CreatePz([FromBody] CreatePzCommand createPzCommand)
        {
            if (_userContextService.GetUserId is not null)
                createPzCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createPzCommand);

            if (result is BaseResponse<PzDto> r)
            {
                if (r.Success)
                {
                    return Created("", r.ReturnedObj);
                }
                else if (r.ValidationErrors?.Any() ?? false)
                {
                    return BadRequest(result.ValidationErrors);
                }
            }
            return BadRequest();
        }

        [HttpPatch("approve-items")]
        public async Task<ActionResult<BaseResponse<PzDto>>> ApprovePzItems([FromBody] ApprovePzItemsCommand approvePzItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePzItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePzItemsCommand);

            if (result is BaseResponse<PzDto> r)
            {
                if (r.Success)
                {
                    return Ok(r.ReturnedObj);
                }
                else if (r.ValidationErrors?.Any() ?? false)
                {
                    return BadRequest(result.ValidationErrors);
                }
            }
            return BadRequest();
        }

        [HttpPatch("approve")]
        public async Task<ActionResult<BaseResponse<PzDto>>> ApprovePz([FromBody] ApprovePzCommand approvePzCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePzCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePzCommand);

            if (result is BaseResponse<PzDto> r)
            {
                if (r.Success)
                {
                    return Ok(r.ReturnedObj);
                }
                else if (r.ValidationErrors?.Any() ?? false)
                {
                    return BadRequest(result.ValidationErrors);
                }
            }
            return BadRequest();
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<PzDto>>> GetSortedAndFilteredPz([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredPzQuery(sieveModel));

            if (result is BaseResponse<PagedResult<PzDto>> r)
            {
                if (r.Success)
                {
                    return Ok(r.ReturnedObj);
                }
                else
                {
                    return BadRequest(r.Message);
                }
            }
            return BadRequest();
        }
    }
}
