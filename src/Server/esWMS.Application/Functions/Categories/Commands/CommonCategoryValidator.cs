using esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands
{
    internal class CommonCategoryValidator<T> : AbstractValidator<T>
        where T : CommonCategoryCommand
    {
        protected readonly IMediator _mediator;

        public CommonCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
