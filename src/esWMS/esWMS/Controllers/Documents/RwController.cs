using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.RwFunctions;
using esWMS.Application.Functions.Documents.RwFunctions.Commands.CreateRw;
using esWMS.Application.Functions.Documents.RwFunctions.Queries.GetSortedFilteredRw;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RwController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RwController> _logger;
        private readonly IUserContextService _userContextService;

        public RwController(
            ILogger<RwController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<RwDto>> CreateRw([FromBody] CreateRwCommand createRwCommand)
        {
            if (_userContextService.GetUserId is not null)
                createRwCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createRwCommand);

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

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<RwDto>>> GetSortedAndFilteredRw([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredRwQuery(sieveModel));

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

        //[HttpPatch("approve-items")]
        //public async Task<ActionResult<RwDto>> ApprovePzItems([FromBody] ApproveWzItemsCommand approveWzItemsCommand)
        //{
        //    if (_userContextService.GetUserId is not null)
        //        approveWzItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

        //    var result = await _mediator.Send(approveWzItemsCommand);

        //    switch (result.Status)
        //    {
        //        case BaseResponse.ResponseStatus.Success:
        //            return Ok(result.ReturnedObj);
        //        case BaseResponse.ResponseStatus.ValidationError:
        //            return BadRequest(result.ValidationErrors);
        //        case BaseResponse.ResponseStatus.ServerError:
        //            return StatusCode(500);
        //        case BaseResponse.ResponseStatus.NotFound:
        //            return NotFound();
        //        case BaseResponse.ResponseStatus.BadQuery:
        //            return BadRequest(result.Message);
        //        default:
        //            return BadRequest();
        //    }
        //}

        //[HttpPatch("approve")]
        //public async Task<ActionResult<WzDto>> ApprovePz([FromBody] ApproveWzCommand approveWzCommand)
        //{
        //    if (_userContextService.GetUserId is not null)
        //        approveWzCommand.ModifiedBy = _userContextService.GetUserId.ToString();

        //    var result = await _mediator.Send(approveWzCommand);

        //    switch (result.Status)
        //    {
        //        case BaseResponse.ResponseStatus.Success:
        //            return Ok(result.ReturnedObj);
        //        case BaseResponse.ResponseStatus.ValidationError:
        //            return BadRequest(result.ValidationErrors);
        //        case BaseResponse.ResponseStatus.ServerError:
        //            return StatusCode(500);
        //        case BaseResponse.ResponseStatus.NotFound:
        //            return NotFound();
        //        case BaseResponse.ResponseStatus.BadQuery:
        //            return BadRequest(result.Message);
        //        default:
        //            return BadRequest();
        //    }
        //}
    }
}
