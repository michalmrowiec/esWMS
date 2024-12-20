﻿using esWMS.API.Controllers.Utils;
using esWMS.API.Services;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.DeleteWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.PurgeEmptyWarehouseUnitsAndItems;
using esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.SetStackOnForWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.UpdateWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetFullWarehouseUnitStack;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetSortedFilteredWarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
using esWMS.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.API.Controllers.WarehouseEnvironment
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseUnitController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WarehouseUnitController> _logger;
        private readonly IUserContextService _userContextService;

        public WarehouseUnitController(
            ILogger<WarehouseUnitController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<WarehouseUnitDto>> CreateWarehouseUnit
            ([FromBody] CreateWarehouseUnitCommand createWarehouseUnit)
        {
            if (_userContextService.GetUserId is not null)
                createWarehouseUnit.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createWarehouseUnit);

            return result.HandleCreatedResult(this, "");
        }

        [HttpGet]
        public async Task<ActionResult<List<WarehouseUnitDto>>> GetWarehouseUnitsDetails
            ([FromQuery] string[] warehouseUnitId)
        {
            var result = await _mediator.Send(new GetWarehouseUnitsByIdsQuery(warehouseUnitId));

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<WarehouseUnitDto>>> GetSortedAndFilteredWarehouseUnits
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehouseUnitsQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpGet("get-stack/{warehouseUnitId}")]
        public async Task<ActionResult<Dictionary<int, WarehouseUnitDto>>> GetFullWarehouseUnitStack
            ([FromRoute] string warehouseUnitId)
        {
            var result = await _mediator.Send(new GetFullWarehouseUnitStackQuery(warehouseUnitId));

            return result.HandleOkResult(this);
        }

        [HttpPatch("set-location")]
        public async Task<ActionResult<WarehouseUnitDto>> SetWarehouseUnitLocation
            ([FromBody] SetLocationForWarehouseUnitCommand setLocationForWarehouseUnitCommand)
        {
            if (_userContextService.GetUserId is not null)
                setLocationForWarehouseUnitCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(setLocationForWarehouseUnitCommand);

            return result.HandleOkResult(this);
        }

        [HttpPatch("set-stack-on")]
        public async Task<ActionResult<WarehouseUnitDto>> SetStackOnForWarehouseUnit
            ([FromBody] SetStackOnForWarehouseUnitCommand setStackOnForWarehouseUnitCommand)
        {
            if (_userContextService.GetUserId is not null)
                setStackOnForWarehouseUnitCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(setStackOnForWarehouseUnitCommand);

            return result.HandleOkResult(this);
        }

        [HttpDelete("{warehouseUnitId}")]
        public async Task<ActionResult> DeleteEmptyWarehouseUnit([FromRoute] string warehouseUnitId)
        {
            var result = await _mediator.Send(new DeleteWarehouseUnitCommand(warehouseUnitId));

            return result.HandleNoContentResult(this);
        }

        [HttpDelete("purge-empty")]
        public async Task<ActionResult> PurgeEmptyWarehouseUnitsAndItems()
        {
            var result = await _mediator.Send(new PurgeEmptyWarehouseUnitsAndItemsCommand());

            return result.HandleNoContentResult(this);
        }

        [HttpPut]
        public async Task<ActionResult<WarehouseUnitDto>> UpdateWarehouseUnit
            ([FromBody] UpdateWarehouseUnitCommand updateWarehouseUnitCommand)
        {
            if (_userContextService.GetUserId is not null)
                updateWarehouseUnitCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(updateWarehouseUnitCommand);

            return result.HandleOkResult(this);
        }
    }
}