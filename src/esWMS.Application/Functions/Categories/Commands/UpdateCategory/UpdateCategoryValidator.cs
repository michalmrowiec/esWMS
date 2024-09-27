using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryValidator : CommonCategoryValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator(IMediator mediator) : base(mediator)
        { }
    }
}
