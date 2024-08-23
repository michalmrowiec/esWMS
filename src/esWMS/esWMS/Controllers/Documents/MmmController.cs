using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.ApproveMmm;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm;
using esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetSortedFilteredMmm;
using esWMS.Application.Responses;
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
        public async Task<ActionResult<BaseResponse<MmmDto>>> CreateMmm
            ([FromBody] CreateMmmCommand createMmmCommand)
        {
            if (_userContextService.GetUserId is not null)
                createMmmCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createMmmCommand);

            if (result is BaseResponse<MmmDto> r)
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

        [HttpPatch("approve")]
        public async Task<ActionResult<BaseResponse<MmmDto>>> ApproveMmm(
            [FromBody] ApproveMmmCommand approveMmmCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveMmmCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveMmmCommand);

            if (result is BaseResponse<MmmDto> r)
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

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<MmmDto>>> GetSortedAndFilteredMmm
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredMmmQuery(sieveModel));

            if (result is BaseResponse<PagedResult<MmmDto>> r)
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
