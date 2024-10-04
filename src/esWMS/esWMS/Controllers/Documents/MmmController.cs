using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.ApproveMmm;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm;
using esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetSortedFilteredMmm;
using esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetListOfWarehouseUnitsInMMM;
using esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById;
using esWMS.Controllers.Utils;
using esWMS.Domain.Models;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MmmController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MmmController> _logger;
        private readonly IUserContextService _userContextService;

        public MmmController(
            ILogger<MmmController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<MmmDto>> CreateMmm
            ([FromBody] CreateMmmCommand createMmmCommand)
        {
            if (_userContextService.GetUserId is not null)
                createMmmCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createMmmCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPost("approve")]
        public async Task<ActionResult<MmmDto>> ApproveMmm(
            [FromBody] ApproveMmmCommand approveMmmCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveMmmCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveMmmCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<MmmDto>>> GetSortedAndFilteredMmm
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredMmmQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpGet]
        public async Task<ActionResult<MmmDto>> GetMmm([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new GetMmmByIdQuery(documentId));

            return result.HandleOkResult(this);
        }

        [HttpGet("warehouse-unit-ids")]
        public async Task<ActionResult<string[]>> GetMmmWarehouseUnitIds([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new GetListOfWarehouseUnitIdsRelatedMMMQuery(documentId));

            return result.HandleOkResult(this);
        }
    }
}
