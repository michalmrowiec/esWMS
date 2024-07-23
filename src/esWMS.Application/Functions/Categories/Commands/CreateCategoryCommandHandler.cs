using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.BaseFunctions.Commands;
using FluentValidation;

namespace esWMS.Application.Functions.Categories.Commands
{
    public class CreateCategoryCommandHandler
        : CreateCommandHandler<CreateCategoryCommand, Category, string, CategoryDto>
    {
        public CreateCategoryCommandHandler
            (AbstractValidator<CreateCategoryCommand> validator,
            IBaseRepository<Category, string> repository,
            IMapper mapper) : base(new CreateCategoryValidator(), repository, mapper)
        { }
    }
}
