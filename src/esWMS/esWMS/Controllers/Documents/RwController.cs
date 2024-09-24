﻿using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.RwFunctions;
using esWMS.Application.Functions.Documents.RwFunctions.Commands.ApproveRw;
using esWMS.Application.Functions.Documents.RwFunctions.Commands.ApproveRwItems;
using esWMS.Application.Functions.Documents.RwFunctions.Commands.CreateRw;
using esWMS.Application.Functions.Documents.RwFunctions.Queries.GetSortedFilteredRw;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.Documents
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RwController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RwController> _logger;
        private readonly IUserContextService _userContextService;

        public RwController(
            ILogger<RwController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<RwDto>> CreateRw([FromBody] CreateRwCommand createRwCommand)
        {
            if (_userContextService.GetUserId is not null)
                createRwCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createRwCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<RwDto>>> GetSortedAndFilteredRw([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredRwQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost("approve-items")]
        public async Task<ActionResult<RwDto>> ApproveRwItems([FromBody] ApproveRwItemsCommand approveRwItemsCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveRwItemsCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveRwItemsCommand);

            return result.HandleOkResult(this);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<RwDto>> ApprovePz([FromBody] ApproveRwCommand approveRwCommand)
        {
            if (_userContextService.GetUserId is not null)
                approveRwCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(approveRwCommand);

            return result.HandleOkResult(this);
        }
    }
}
