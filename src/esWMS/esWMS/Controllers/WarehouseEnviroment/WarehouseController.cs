using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse;
using esWMS.Application.Functions.Warehouses.Commands.UpdateWarehouse;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Functions.Warehouses.Queries.GetSortedFilteredWarehouseStocks;
using esWMS.Controllers.Utils;
using esWMS.Domain.Models;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.WarehouseEnviroment
{
    [Authorize]
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
        public async Task<ActionResult<PagedResult<WarehouseDto>>> GetSortedAndFilteredWarehouses(
            [FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehousesQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered-stocks")]
        public async Task<ActionResult<PagedResult<WarehouseStockDto>>> GetSortedAndFilteredWarehouseStocks(
            [FromBody] SieveModel sieveModel, [FromQuery] string? warehouseId = null)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehouseStocksQuery(sieveModel, warehouseId));

            return result.HandleOkResult(this);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpPost]
        public async Task<ActionResult<WarehouseDto>> CreateWarehouse(
            [FromBody] CreateWarehouseCommand createWarehouseCommand)
        {
            if (_userContextService.GetUserId is not null)
                createWarehouseCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createWarehouseCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPut]
        public async Task<ActionResult<WarehouseDto>> UpdateWarehouse(
            [FromBody] UpdateWarehouseCommand updateWarehouseCommand)
        {
            if (_userContextService.GetUserId is not null)
                updateWarehouseCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(updateWarehouseCommand);

            return result.HandleOkResult(this);
        }
    }
}
