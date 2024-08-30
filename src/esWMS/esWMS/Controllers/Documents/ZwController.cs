using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.ZwFunctions;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZw;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZwItems;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw;
using esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetSortedFilteredZw;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ZwController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ZwController> _logger;
        private readonly IUserContextService _userContextService;

        public ZwController(
            ILogger<ZwController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<ZwDto>> CreateZw([FromBody] CreateZwCommand createZwCommand)
        {
            if (_userContextService.GetUserId is not null)
                createZwCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createZwCommand);

            switch (result.Status)
            {
                case BaseResponse.ResponseStatus.Success:
                    return Created("", result.ReturnedObj);
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

        [HttpPatch("approve-items")]
        public async Task<ActionResult<ZwDto>> ApproveZwItems([FromBody] ApproveZwItemsCommand approveZwItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveZwItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveZwItemsCommand);

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

        [HttpPatch("approve")]
        public async Task<ActionResult<ZwDto>> ApproveZw([FromBody] ApproveZwCommand approveZwCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveZwCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveZwCommand);

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

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<ZwDto>>> GetSortedAndFilteredZw([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredZwQuery(sieveModel));

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
