using esMWS.Domain.Models;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly IUserContextService _userContextService;

        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetSortedAndFilteredProducts([FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredProductsQuery(sieveModel));

            if (result is BaseResponse<PagedResult<ProductDto>> r)
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
