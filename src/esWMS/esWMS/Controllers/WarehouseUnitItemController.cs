using esMWS.Domain.Models;
using esWMS.Application.Functions.WarehouseUnitItems;
using esWMS.Application.Functions.WarehouseUnitItems.Queries.GetSortedFilteredWarehouseUnitItems;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseUnitItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WarehouseUnitItemController> _logger;
        private readonly IUserContextService _userContextService;

        public WarehouseUnitItemController(
            ILogger<WarehouseUnitItemController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<WarehouseUnitItemDto>>> GetSortedAndFilteredWarehouseUnitItems
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehouseUnitItemsQuery(sieveModel));

            return result.HandleOkResult(this);
        }
    }
}
