using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.BaseFunctions.Commands;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    internal class CreateProductCommandHandler
        : CreateCommandHandler<CreateProductCommand, Product, string, ProductDto>
    {
        public CreateProductCommandHandler
            (IBaseRepository<Product, string> repository,
            IMapper mapper) : base(new CreateProductValidator(), repository, mapper)
        { }
    }
}
