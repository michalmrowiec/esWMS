using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetSortedFilteredWarehouseUnits;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;

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

            if (result is BaseResponse<WarehouseUnitDto> r)
            {
                if (r.Success)
                {
                    return Created("", r.ReturnedObj);
                }
                else if (r.ValidationErrors?.Any() ?? false)
                {
                    return BadRequest(result.ValidationErrors);
                }
            }
            return BadRequest();
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<WarehouseUnitDto>>> GetSortedAndFilteredWarehouseUnits([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWarehouseUnitsQuery(sieveModel));

            if (result is BaseResponse<PagedResult<WarehouseUnitDto>> r)
            {
                if (r.Success)
                {
                    return Ok(r.ReturnedObj);
                }
                else
                {
                    return BadRequest(r.Message);
                }
            }
            return BadRequest();
        }
    }
}
