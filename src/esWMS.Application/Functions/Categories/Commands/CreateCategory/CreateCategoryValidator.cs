using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.CreateCategory
{
    internal class CreateCategoryValidator : CommonCategoryValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator(IMediator mediator) : base(mediator)
        { }
    }
}
