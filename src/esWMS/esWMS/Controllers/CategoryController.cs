﻿using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;
        private readonly IUserContextService _userContextService;

        public CategoryController(
            ILogger<CategoryController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            if (_userContextService.GetUserId is not null)
                createCategoryCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createCategoryCommand);

            if (result is BaseResponse<CategoryDto> r)
            {
                return Created("", r.ReturnedObj);
            }

            return BadRequest(result.ValidationErrors);
        }
    }
}