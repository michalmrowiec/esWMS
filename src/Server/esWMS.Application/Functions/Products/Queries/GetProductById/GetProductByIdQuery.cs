using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(string ProductId)
        : IRequest<BaseResponse<ProductDto>>;
}
