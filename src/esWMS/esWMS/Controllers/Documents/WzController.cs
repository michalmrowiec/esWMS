using esWMS.Application.Functions.Documents.WzFunctions;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWz;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWzItems;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.DeleteWz;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.DeleteWzItem;
using esWMS.Application.Functions.Documents.WzFunctions.Queries.GetSortedFilteredWz;
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
    public class WzController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WzController> _logger;
        private readonly IUserContextService _userContextService;

        public WzController(
            ILogger<WzController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<WzDto>> CreatePz([FromBody] CreateWzCommand createWzCommand)
        {
            if (_userContextService.GetUserId is not null)
                createWzCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createWzCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPost("approve-items")]
        public async Task<ActionResult<WzDto>> ApprovePzItems([FromBody] ApproveWzItemsCommand approveWzItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveWzItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveWzItemsCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<WzDto>> ApprovePz([FromBody] ApproveWzCommand approveWzCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveWzCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveWzCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<WzDto>>> GetSortedAndFilteredWz([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredWzQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpDelete("item/{documentItemId}")]
        public async Task<ActionResult> DeleteWzDocumentItem([FromRoute] string documentItemId)
        {
            var result = await _mediator.Send(new DeleteWzItemCommand(documentItemId));

            return result.HandleNoContentResult(this);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWzDocument([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new DeleteWzCommand(documentId));

            return result.HandleNoContentResult(this);
        }
    }
}
