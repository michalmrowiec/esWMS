using esWMS.Domain.Models;
using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Functions.Documents.MmpFunctions.Commands.ApproveMmp;
using esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetListOfWarehouseUnitsInMMP;
using esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetSortedFilteredMmp;
using esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MmpController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MmpController> _logger;
        private readonly IUserContextService _userContextService;

        public MmpController(
            ILogger<MmpController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<MmpDto>>> GetSortedAndFilteredMmp
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredMmpQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<MmpDto>> ApproveMmp(
            [FromBody] ApproveMmpCommand approveMmpCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveMmpCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveMmpCommand);

            return result.HandleOkResult(this);
        }

        [HttpGet]
        public async Task<ActionResult<MmpDto>> GetMmp([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new GetMmpByIdQuery(documentId));

            return result.HandleOkResult(this);
        }

        [HttpGet("warehouse-unit-ids")]
        public async Task<ActionResult<string[]>> GetMmpWarehouseUnitIds([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new GetListOfWarehouseUnitIdsRelatedMMPQuery(documentId));

            return result.HandleOkResult(this);
        }
    }
}
