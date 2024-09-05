using esMWS.Domain.Models;
using esWMS.Application.Functions.Zones;
using esWMS.Application.Functions.Zones.Commands.CreateZone;
using esWMS.Application.Functions.Zones.Queries.GetSortedFilteredZones;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ZoneController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ZoneController> _logger;
        private readonly IUserContextService _userContextService;

        public ZoneController(
            ILogger<ZoneController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<ZoneDto>>> GetSortedAndFilteredZones
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredZonesQuery(sieveModel));

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

        [HttpPost]
        public async Task<ActionResult<ZoneDto>> CreateLocation
            ([FromBody] CreateZoneCommand createZoneCommand)
        {
            if (_userContextService.GetUserId is not null)
                createZoneCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createZoneCommand);

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
    }
}
