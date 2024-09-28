using esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryValidator : CommonCategoryValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator(IMediator mediator) : base(mediator)
        {
            RuleFor(x => x)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var sm = new Sieve.Models.SieveModel()
                    {
                        Filters = $"CategoryName==*{value.CategoryName}",
                        Sorts = "",
                        Page = 1,
                        PageSize = 1
                    };

                    var response = await _mediator.Send(new GetSortedFilteredCategoriesQuery(sm));

                    if (!response.ReturnedObj.Items.All(x => x.CategoryId.Equals(value.CategoryId)))
                    {
                        context.AddFailure("CategoryName", $"A category with that name already exists.");
                    }
                });
        }
    }
}
