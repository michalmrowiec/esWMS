using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions;
using esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetEligibleItemsForZwReturn;
using esWMS.Application.Functions.Products;
using FluentValidation;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw
{
    internal class CreateZwValidator : CreateFlatBaseDocumentValidator<CreateZwCommand>
    {
        public CreateZwValidator(IList<DocumentItemDto> eligibleItemsForZwReturn)
        {
            RuleFor(x => x.DocumentItemIdQuantity)
                .NotEmpty()
                .WithMessage("DocumentItemIdQuantity cannot be empty");

            RuleFor(x => x.DocumentItemIdQuantity)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    foreach (var item in value)
                    {
                        var eligibleItemForZwReturn = eligibleItemsForZwReturn.First(x => x.DocumentItemId.Equals(item.DocumentItemId));

                        if (eligibleItemForZwReturn.Quantity < item.Quantity)
                        {
                            context.AddFailure(
                                "DocumentItemIdQuantity",
                                $"The quantity of document item ID {item.DocumentItemId} to be returned ({item.Quantity}) exceeds the available quantity ({eligibleItemsForZwReturn.First().Quantity}) issued by RW.");
                        }
                    }
                });
        }
    }
}
