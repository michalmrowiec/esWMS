using esWMS.Application.Functions.Documents.WzFunctions;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWz;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWzItems;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<BaseResponse<WzDto>>> CreatePz([FromBody] CreateWzCommand createWzCommand)
        {
            if (_userContextService.GetUserId is not null)
                createWzCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createWzCommand);

            if (result is BaseResponse<WzDto> r)
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

        [HttpPatch("/approve-wz-items")]
        public async Task<ActionResult<BaseResponse<WzDto>>> ApprovePzItems([FromBody] ApproveWzItemsCommand approveWzItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveWzItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveWzItemsCommand);

            if (result is BaseResponse<WzDto> r)
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

        [HttpPatch("/approve-wz")]
        public async Task<ActionResult<BaseResponse<WzDto>>> ApprovePz([FromBody] ApproveWzCommand approveWzCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveWzCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveWzCommand);

            if (result is BaseResponse<WzDto> r)
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

        //[HttpPost("get-filtered")]
        //public async Task<ActionResult<PagedResult<WzDto>>> GetSortedAndFilteredPz([FromBody] SieveModel sieveModel)
        //{
        //    var result = await _mediator.Send(new GetSortedFilteredPzQuery(sieveModel));

        //    if (result is BaseResponse<PagedResult<WzDto>> r)
        //    {
        //        if (r.Success)
        //        {
        //            return Ok(r.ReturnedObj);
        //        }
        //        else
        //        {
        //            return BadRequest(r.Message);
        //        }
        //    }
        //    return BadRequest();
        //}
    }
}
