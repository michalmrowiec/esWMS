using esMWS.Domain.Models;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.DeleteWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.SetStackOnForWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetSortedFilteredWarehouseUnits;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
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
        public async Task<ActionResult<PagedResult<WarehouseUnitDto>>> GetSortedAndFilteredWarehouseUnits([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehouseUnitsQuery(sieveModel));

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

        [HttpPatch("set-location")]
        public async Task<ActionResult<WarehouseUnitDto>> SetWarehouseUnitLocation
            ([FromBody] SetLocationForWarehouseUnitCommand setLocationForWarehouseUnitCommand)
        {
            if (_userContextService.GetUserId is not null)
                setLocationForWarehouseUnitCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(setLocationForWarehouseUnitCommand);

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

        [HttpPatch("set-stack-on")]
        public async Task<ActionResult<WarehouseUnitDto>> SetStackOnForWarehouseUnit
            ([FromBody] SetStackOnForWarehouseUnitCommand setStackOnForWarehouseUnitCommand)
        {
            if (_userContextService.GetUserId is not null)
                setStackOnForWarehouseUnitCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(setStackOnForWarehouseUnitCommand);

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

        [HttpDelete("{warehouseUnitId}")]
        public async Task<ActionResult> DeleteEmptyWarehouseUnit([FromRoute] string warehouseUnitId)
        {
            var result = await _mediator.Send(new DeleteWarehouseUnitCommand(warehouseUnitId));

            switch (result.Status)
            {
                case BaseResponse.ResponseStatus.Success:
                    return NoContent();
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
