using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(string CategoryId) : IRequest<BaseResponse>;
}
