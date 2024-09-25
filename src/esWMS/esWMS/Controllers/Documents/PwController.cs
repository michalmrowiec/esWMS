using esWMS.Domain.Models;
using esWMS.Application.Functions.Documents.PwFunctions;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePw;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePwItems;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw;
using esWMS.Application.Functions.Documents.PwFunctions.Queries.GetSortedFilteredPw;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PwController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PwController> _logger;
        private readonly IUserContextService _userContextService;

        public PwController(
            ILogger<PwController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<PwDto>> CreatePw([FromBody] CreatePwCommand createPwCommand)
        {
            if (_userContextService.GetUserId is not null)
                createPwCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createPwCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPost("approve-items")]
        public async Task<ActionResult<PwDto>> ApprovePwItems([FromBody] ApprovePwItemsCommand approvePwItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePwItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePwItemsCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<PwDto>> ApprovePw([FromBody] ApprovePwCommand approvePwCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePwCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePwCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<PwDto>>> GetSortedAndFilteredPw([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredPwQuery(sieveModel));

            return result.HandleOkResult(this);
        }
    }
}
