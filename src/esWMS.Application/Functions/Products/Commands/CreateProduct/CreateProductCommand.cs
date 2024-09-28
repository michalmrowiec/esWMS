using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommand : CommonProductCommand, IRequest<BaseResponse<ProductDto>>
    {
        public string? CreatedBy { get; set; }
    }
}
