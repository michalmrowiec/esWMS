using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler
        (ICategoryRepository repository,
        IMapper mapper,
        IMediator mediator)
        : IRequestHandler<UpdateCategoryCommand, BaseResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<CategoryDto>> Handle
            (UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new UpdateCategoryValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<CategoryDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.CategoryId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<CategoryDto>(updated);

                return new BaseResponse<CategoryDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<CategoryDto>
                    (BaseResponse.ResponseStatus.NotFound, "Category not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
