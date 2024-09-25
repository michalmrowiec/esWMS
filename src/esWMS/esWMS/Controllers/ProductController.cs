using esWMS.Domain.Models;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Controllers.Utils;
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

            return result.HandleOkResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct
            ([FromBody] CreateProductCommand createProductCommand)
        {
            if (_userContextService.GetUserId is not null)
                createProductCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createProductCommand);

            return result.HandleCreatedResult(this, "");
        }
    }
}
