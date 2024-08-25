using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.ApproveMmm;
using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetSortedFilteredMmp;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using esWMS.Application.Functions.Documents.MmpFunctions.Commands.ApproveMmp;

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

            if (result is BaseResponse<PagedResult<MmpDto>> r)
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

        [HttpPatch("approve")]
        public async Task<ActionResult<BaseResponse<MmpDto>>> ApproveMmp(
            [FromBody] ApproveMmpCommand approveMmpCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveMmpCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveMmpCommand);

            if (result is BaseResponse<MmpDto> r)
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
