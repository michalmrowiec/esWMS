using esMWS.Domain.Models;
using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Functions.Warehouses.Queries.GetSortedFilteredWarehouseStocks;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WarehouseController> _logger;
        private readonly IUserContextService _userContextService;

        public WarehouseController(
            ILogger<WarehouseController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<WarehouseDto>>> GetSortedAndFilteredWarehouses([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehousesQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered-stocks")]
        public async Task<ActionResult<PagedResult<WarehouseStockDto>>> GetSortedAndFilteredWarehouseStocks
            ([FromBody] SieveModel sieveModel, [FromQuery] string? warehouseId = null)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehouseStocksQuery(sieveModel, warehouseId));

            return result.HandleOkResult(this);
        }
    }
}
