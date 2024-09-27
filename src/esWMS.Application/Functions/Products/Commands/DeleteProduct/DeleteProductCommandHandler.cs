using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.WarehouseUnitItems.Queries.GetSortedFilteredWarehouseUnitItems;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.WarehouseEnviroment;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler
        (IProductRepository repository,
        IMediator mediator,
        IMapper mapper)
        : IRequestHandler<DeleteProductCommand, BaseResponse>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product;
            try
            {
                product = await _repository.GetByIdAsync(request.ProductId);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.NotFound, "Product not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var wuiResponse = await _mediator.Send(
                new GetSortedFilteredWarehouseUnitItemsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 500,
                        Filters = "ProductId==" + request.ProductId
                    }));

            var wui = wuiResponse.ReturnedObj?.Items ?? [];

            if (!wuiResponse.IsSuccess())
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var validationResult = await new DeleteProductValidator
                (wui).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse(validationResult);
            }

            try
            {
                await _repository.DeleteAsync(product.ProductId);
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
