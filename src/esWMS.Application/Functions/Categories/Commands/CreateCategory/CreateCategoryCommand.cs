using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand
        : CommonCategoryCommand, IRequest<BaseResponse<CategoryDto>>
    {
        public string? CreatedBy { get; set; }
    }
}
