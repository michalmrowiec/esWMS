using esWMS.API.Controllers.Utils;
using esWMS.API.Services;
using esWMS.Application.Functions.Documents.ZwFunctions;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZw;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZwItems;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.DeleteZw;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.DeleteZwItem;
using esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetEligibleItemsForZwReturn;
using esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetSortedFilteredZw;
using esWMS.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.API.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ZwController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ZwController> _logger;
        private readonly IUserContextService _userContextService;

        public ZwController(
            ILogger<ZwController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<ZwDto>> CreateZw([FromBody] CreateZwCommand createZwCommand)
        {
            if (_userContextService.GetUserId is not null)
                createZwCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createZwCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPost("approve-items")]
        public async Task<ActionResult<ZwDto>> ApproveZwItems
            ([FromBody] ApproveZwItemsCommand approveZwItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveZwItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveZwItemsCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<ZwDto>> ApproveZw
            ([FromBody] ApproveZwCommand approveZwCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveZwCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveZwCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<ZwDto>>> GetSortedAndFilteredZw
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredZwQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost("get-eligible-items")]
        public async Task<ActionResult<PagedResult<ZwDto>>> GetEligibleItemsForZwReturn
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetEligibleItemsForZwReturnQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpDelete("item/{documentItemId}")]
        public async Task<ActionResult> DeleteZwDocumentItem([FromRoute] string documentItemId)
        {
            var result = await _mediator.Send(new DeleteZwItemCommand(documentItemId));

            return result.HandleNoContentResult(this);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteZwDocument([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new DeleteZwCommand(documentId));

            return result.HandleNoContentResult(this);
        }
    }
}
