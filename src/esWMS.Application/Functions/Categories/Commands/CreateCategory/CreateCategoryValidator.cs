using esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IMediator _mediator;

        public CreateCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x.CategoryName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50).
                CustomAsync(async (value, context, cancellationToken) =>
                {
                    var sm = new Sieve.Models.SieveModel()
                    {
                        Filters = $"CategoryName==*{value}",
                        Sorts = "",
                        Page = 1,
                        PageSize = 1
                    };

                    var response = await _mediator.Send(new GetSortedFilteredCategoriesQuery(sm));

                    if(response.ReturnedObj.Items.Any())
                    {
                        context.AddFailure("CategoryName", $"A category with that name already exists.");
                    }
                });
        }
    }
}
