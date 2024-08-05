using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePz;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DocumentController> _logger;
        private readonly IUserContextService _userContextService;

        public DocumentController(
            ILogger<DocumentController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<PzDto>>> CreatePz([FromBody] CreatePzCommand createPzCommand)
        {
            if (_userContextService.GetUserId is not null)
                createPzCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createPzCommand);

            if (result is BaseResponse<PzDto> r)
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

        [HttpPatch("/approve-pz-items")]
        public async Task<ActionResult<BaseResponse<PzDto>>> ApprovePzItems([FromBody] ApprovePzItemsCommand approvePzItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePzItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePzItemsCommand);

            if (result is BaseResponse<PzDto> r)
            {
                if (r.Success)
                {
                    return Ok(r.ReturnedObj);
                }
                else if (r.ValidationErrors?.Any() ?? false)
                {
                    return BadRequest(result.ValidationErrors);
                }
            }
            return BadRequest();
        }

        [HttpPatch("/approve-pz")]
        public async Task<ActionResult<BaseResponse<PzDto>>> ApprovePz([FromBody] ApprovePzCommand approvePzCommand)
        {
            if (_userContextService.GetUserId is not null)
                approvePzCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approvePzCommand);

            if (result is BaseResponse<PzDto> r)
            {
                if (r.Success)
                {
                    return Ok(r.ReturnedObj);
                }
                else if (r.ValidationErrors?.Any() ?? false)
                {
                    return BadRequest(result.ValidationErrors);
                }
            }
            return BadRequest();
        }
    }
}
