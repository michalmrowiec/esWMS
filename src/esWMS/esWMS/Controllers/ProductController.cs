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

            switch (result.Status)
            {
                case BaseResponse.ResponseStatus.Success:
                    return Ok(result.ReturnedObj);
                case BaseResponse.ResponseStatus.ValidationError:
                    return BadRequest(result.ValidationErrors);
                case BaseResponse.ResponseStatus.ServerError:
                    return StatusCode(500);
                case BaseResponse.ResponseStatus.NotFound:
                    return NotFound();
                case BaseResponse.ResponseStatus.BadQuery:
                    return BadRequest(result.Message);
                default:
                    return BadRequest();
            }
        }
    }
}
