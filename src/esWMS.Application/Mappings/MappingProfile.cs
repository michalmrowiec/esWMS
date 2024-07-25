using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse;
using esWMS.Application.Functions.WarehouseUnitItems;
using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;

namespace esWMS.Application.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateWarehouseCommand, Warehouse>();
            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<CreateWarehouseUnitCommand, WarehouseUnit>();
            CreateMap<WarehouseUnit, WarehouseUnitDto>()
                .ForMember(dto => dto.WarehouseUnitItems, opt => opt.MapFrom(src => src.WarehouseUnitItems));

            CreateMap<CreateWarehouseUnitItemCommand, WarehouseUnitItem>();
            CreateMap<WarehouseUnitItem, WarehouseUnitItemDto>();
        }
    }
}
