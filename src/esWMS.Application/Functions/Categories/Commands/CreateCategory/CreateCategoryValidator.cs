using esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.CreateCategory
{
    internal class CreateCategoryValidator : CommonCategoryValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator(IMediator mediator) : base(mediator)
        {
            RuleFor(x => x.CategoryName)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var sm = new Sieve.Models.SieveModel()
                    {
                        Filters = $"CategoryName==*{value}",
                        Sorts = "",
                        Page = 1,
                        PageSize = 1
                    };

                    var response = await _mediator.Send(new GetSortedFilteredCategoriesQuery(sm));

                    if (response.ReturnedObj.Items.Any())
                    {
                        context.AddFailure("CategoryName", $"A category with that name already exists.");
                    }
                });
        }
    }
}
