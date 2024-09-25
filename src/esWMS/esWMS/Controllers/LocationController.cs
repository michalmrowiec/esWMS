using esWMS.Domain.Models;
using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.Locations.Commands.CreateLocation;
using esWMS.Application.Functions.Locations.Queries.GetSortedFilteredLocations;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LocationController> _logger;
        private readonly IUserContextService _userContextService;

        public LocationController(
            ILogger<LocationController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<LocationDto>>> GetSortedAndFilteredLocations
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredLocationsQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDto>> CreateLocation
            ([FromBody] CreateLocationCommand createLocationCommand)
        {
            if (_userContextService.GetUserId is not null)
                createLocationCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createLocationCommand);

            return result.HandleCreatedResult(this, "");
        }
    }
}
