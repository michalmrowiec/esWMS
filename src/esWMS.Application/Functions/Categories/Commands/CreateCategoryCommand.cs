using esWMS.Application.Functions.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<BaseResponse<CategoryDto>>
    {
        public string CategoryName { get; set; } = null!;
        public string? ParentCategoryId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
