using esWMS.API.Controllers.Utils;
using esWMS.API.Services;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePz;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.DeletePz;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.DeletePzItem;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.API.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PzController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PzController> _logger;
        private readonly IUserContextService _userContextService;

        public PzController(
            ILogger<PzController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<PzDto>> CreatePz([FromBody] CreatePzCommand createPzCommand)
        {
            if (_userContextService.GetUserId is not null)
                createPzCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createPzCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPost("approve-items")]
        public async Task<ActionResult<PzDto>> ApprovePzItems([FromBody] ApprovePzItemsCommand approvePzItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePzItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePzItemsCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<PzDto>> ApprovePz([FromBody] ApprovePzCommand approvePzCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePzCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePzCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<PzDto>>> GetSortedAndFilteredPz([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredPzQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpDelete("item/{documentItemId}")]
        public async Task<ActionResult> DeletePzDocumentItem([FromRoute] string documentItemId)
        {
            var result = await _mediator.Send(new DeletePzItemCommand(documentItemId));

            return result.HandleNoContentResult(this);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePzDocument([FromQuery] string documentId)
        {
            var result = await _mediator.Send(new DeletePzCommand(documentId));

            return result.HandleNoContentResult(this);
        }
    }
}
