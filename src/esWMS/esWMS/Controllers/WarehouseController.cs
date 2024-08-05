using esMWS.Domain.Models;
using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Responses;
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

            if (result is BaseResponse<PagedResult<WarehouseDto>> r)
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
