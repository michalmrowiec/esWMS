using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.WarehouseEnviroment;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryCommandHandler
        (ICategoryRepository repository,
        IMapper mapper,
        IMediator mediator)
        : IRequestHandler<DeleteCategoryCommand, BaseResponse>
    {
        private readonly ICategoryRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category;
            try
            {
                category = await _repository.GetByIdAsync(request.CategoryId);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.NotFound, "Category not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var productResponse = await _mediator.Send(
                new GetSortedFilteredProductsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 500,
                        Filters = "CategoryId==" + request.CategoryId
                    }));

            var productWithCategory = productResponse.ReturnedObj?.Items ?? [];

            if (!productResponse.IsSuccess())
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var validationResult = await new DeleteCategoryValidator
                (productWithCategory).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse(validationResult);
            }

            try
            {
                await _repository.DeleteAsync(category.CategoryId);
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
