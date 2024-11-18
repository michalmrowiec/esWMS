using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(string ProductId) : IRequest<BaseResponse>;
}
