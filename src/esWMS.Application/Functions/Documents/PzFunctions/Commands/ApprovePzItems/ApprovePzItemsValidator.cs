using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
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
                    var contained = value.DocumentItemsWithAssigment.Select(x => x.DocumentItemId).Except(documentItemIds);

                    if (contained.Any())
                    {
                        context.AddFailure(
                            "DocumentItemsId",
                            $"The original document does not contain these item identifiers: {string.Join("; ", contained)}");
                    }

                    string[] warehouseUnitIds = value.DocumentItemsWithAssigment
                                                    .Select(x => x.WarehouseUnitId)
                                                    .Where(x => x != null)
                                                    .OfType<string>()
                                                    .ToArray();

                    if (warehouseUnitIds.Length != 0)
                    {
                        var warehouseUnitResponse = await _mediator.Send(
                            new GetWarehouseUnitsByIdsQuery(warehouseUnitIds));
                        var warehouseUnit = warehouseUnitResponse.ReturnedObj;

                        if (!warehouseUnitResponse.Success || warehouseUnit == null)
                        {
                            context.AddFailure("Somenthing went wrong");
                        }

                        var warehouseUnitIdsResponse = warehouseUnit!.Select(x => x.WarehouseUnitId).ToArray();
                        var warehouseUnitIdsContained = warehouseUnitIds.Except(warehouseUnitIdsResponse);

                        if (warehouseUnitIdsContained.Any())
                        {
                            context.AddFailure(
                                "WarehouseUnitIds",
                                $"There are no warehouse unit with identifiers: {string.Join("; ", warehouseUnitIdsContained)}");
                        }
                    }
                });
        }
    }
}
