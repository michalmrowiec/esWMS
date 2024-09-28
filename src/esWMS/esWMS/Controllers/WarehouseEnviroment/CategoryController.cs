using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Functions.Categories.Commands.DeleteCategory;
using esWMS.Application.Functions.Categories.Commands.UpdateCategory;
using esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories;
using esWMS.Controllers.Utils;
using esWMS.Domain.Models;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers.WarehouseEnviroment
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

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetSortedAndFilteredCategories
            ([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredCategoriesQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory
            ([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            if (_userContextService.GetUserId is not null)
                createCategoryCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createCategoryCommand);

            return result.HandleCreatedResult(this, "");
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory
            ([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            if (_userContextService.GetUserId is not null)
                updateCategoryCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(updateCategoryCommand);

            return result.HandleOkResult(this);
        }


        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] string categoryId)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(categoryId));

            return result.HandleNoContentResult(this);
        }
    }
}
