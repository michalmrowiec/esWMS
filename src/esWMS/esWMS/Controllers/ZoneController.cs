using esWMS.Domain.Models;
using esWMS.Application.Functions.Zones;
using esWMS.Application.Functions.Zones.Commands.CreateZone;
using esWMS.Application.Functions.Zones.Queries.GetSortedFilteredZones;
using esWMS.Controllers.Utils;
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

            return result.HandleOkResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<ZoneDto>> CreateLocation
            ([FromBody] CreateZoneCommand createZoneCommand)
        {
            if (_userContextService.GetUserId is not null)
                createZoneCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createZoneCommand);

            return result.HandleCreatedResult(this, "");
        }
    }
}
