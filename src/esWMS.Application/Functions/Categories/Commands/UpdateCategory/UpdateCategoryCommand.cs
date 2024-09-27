using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand
        : CommonCategoryCommand, IRequest<BaseResponse<CategoryDto>>
    {
        public string CategoryId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
