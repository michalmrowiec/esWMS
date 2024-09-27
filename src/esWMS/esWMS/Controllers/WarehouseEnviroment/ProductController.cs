using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Functions.Products.Commands.DeleteProduct;
using esWMS.Application.Functions.Products.Commands.UpdateProduct;
using esWMS.Application.Functions.Products.Queries.GetProductById;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
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

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProductDetails
            ([FromQuery] string productId)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(productId));

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetSortedAndFilteredProducts
            ([FromBody] SieveModel sieveModel)
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

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct
            ([FromBody] UpdateProductCommand updateProductCommand)
        {
            if (_userContextService.GetUserId is not null)
                updateProductCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(updateProductCommand);

            return result.HandleOkResult(this);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteEmptyWarehouseUnit([FromRoute] string productId)
        {
            var result = await _mediator.Send(new DeleteProductCommand(productId));

            return result.HandleNoContentResult(this);
        }
    }
}
