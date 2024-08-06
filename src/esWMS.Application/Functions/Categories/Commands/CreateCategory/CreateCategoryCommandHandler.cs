using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, BaseResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateCategoryCommandHandler
            (ICategoryRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BaseResponse<CategoryDto>> Handle
            (CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateCategoryValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<CategoryDto>(validationResult);
            }

            var entity = _mapper.Map<Category>(request);

            entity.CategoryId = Guid.NewGuid().ToString();

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<CategoryDto>(createdEntity);

            return new BaseResponse<CategoryDto>(entityDto);
        }
    }
}
