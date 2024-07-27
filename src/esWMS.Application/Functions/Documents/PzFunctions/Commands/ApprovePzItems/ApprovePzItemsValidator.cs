using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems
{
    internal class ApprovePzItemsValidator : AbstractValidator<ApprovePzItemsCommand>
    {
        private readonly IMediator _mediator;
        public ApprovePzItemsValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var documentResponse = await _mediator.Send(new GetDocumentByIdQuery(value.DocumentId));
                    var document = documentResponse.ReturnedObj;

                    if (!documentResponse.Success || document == null)
                    {
                        context.AddFailure("DocumentId", $"The document by Id: {value} does not exist.");
                    }

                    var documentItemIds = document!.DocumentItems.Select(x => x.DocumentItemsId).ToArray();
                    var contained = value.DocumentItemsId.Except(documentItemIds);

                    if (contained.Any())
                    {
                        context.AddFailure(
                            "DocumentItemsId",
                            $"The original document does not contain these item identifiers: {string.Join("; ", contained)}");
                    }
                });
        }
    }
}
