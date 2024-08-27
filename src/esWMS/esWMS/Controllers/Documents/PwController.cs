using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.PwFunctions;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePw;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePwItems;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw;
using esWMS.Application.Functions.Documents.PwFunctions.Queries.GetSortedFilteredPw;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PwController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PwController> _logger;
        private readonly IUserContextService _userContextService;

        public PwController(
            ILogger<PwController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<PwDto>> CreatePw([FromBody] CreatePwCommand createPwCommand)
        {
            if (_userContextService.GetUserId is not null)
                createPwCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createPwCommand);

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
        public async Task<ActionResult<PwDto>> ApprovePwItems([FromBody] ApprovePwItemsCommand approvePwItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePwItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePwItemsCommand);

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
        public async Task<ActionResult<PwDto>> ApprovePw([FromBody] ApprovePwCommand approvePwCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePwCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePwCommand);

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
        public async Task<ActionResult<PagedResult<PwDto>>> GetSortedAndFilteredPw([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredPwQuery(sieveModel));

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
