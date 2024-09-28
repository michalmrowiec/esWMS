using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : CommonProductCommand, IRequest<BaseResponse<ProductDto>>
    {
        public string ProductId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
