using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Contractors.Commands.CreateContractor;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm;
using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Functions.Documents.PwFunctions;
using esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Functions.Documents.RwFunctions;
using esWMS.Application.Functions.Documents.RwFunctions.Commands.CreateRw;
using esWMS.Application.Functions.Documents.WzFunctions;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz;
using esWMS.Application.Functions.Documents.ZwFunctions;
using esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw;
using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.Locations.Commands.CreateLocation;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse;
using esWMS.Application.Functions.WarehouseUnitItems;
using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using esWMS.Application.Functions.WarehouseUnitItems.Commands.UpdateWarehouseUnitItem;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;
using esWMS.Application.Functions.WarehouseUnits.Commands.UpdateWarehouseUnit;
using esWMS.Application.Functions.Zones;
using esWMS.Application.Functions.Zones.Commands.CreateZone;

namespace esWMS.Application.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<CreateWarehouseCommand, Warehouse>();
            CreateMap<Warehouse, FlatWarehouseDto>();
            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<CreateWarehouseUnitCommand, WarehouseUnit>();
            CreateMap<CreateFlatWarehouseUnitCommand, WarehouseUnit>();
            CreateMap<UpdateWarehouseUnitCommand, WarehouseUnit>();
            CreateMap<WarehouseUnit, WarehouseUnitDto>()
                .ForMember(dto => dto.WarehouseUnitItems,
                           opt => opt.MapFrom(src => src.WarehouseUnitItems));

            CreateMap<WarehouseUnitDto, WarehouseUnit>()
                .ForMember(dest => dest.Warehouse, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.StackOn, opt => opt.Ignore())
                .ForMember(dest => dest.WarehouseUnitItems, opt => opt.MapFrom(src => src.WarehouseUnitItems));

            CreateMap<WarehouseUnit, FlatWarehouseUnitDto>();

            CreateMap<CreateWarehouseUnitItemCommand, WarehouseUnitItem>();
            CreateMap<UpdateWarehouseUnitItemCommand, WarehouseUnitItem>();
            CreateMap<WarehouseUnitItem, WarehouseUnitItemDto>();
            CreateMap<WarehouseUnitItemDto, WarehouseUnitItem>()
                .ForMember(dest => dest.WarehouseUnit, opt => opt.MapFrom(src => src.WarehouseUnit));

            CreateMap<FlatWarehouseUnitDto, WarehouseUnit>()
                .ForMember(dest => dest.Warehouse, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.StackOn, opt => opt.Ignore())
                .ForMember(dest => dest.WarehouseUnitItems, opt => opt.Ignore());

            CreateMap<CreateZoneCommand, Zone>();
            CreateMap<Zone, FlatZoneDto>();
            CreateMap<Zone, ZoneDto>();

            CreateMap<CreateLocationCommand, Location>();
            CreateMap<Location, FlatLocationDto>();
            CreateMap<Location, LocationDto>()
                .ReverseMap();

            CreateMap<CreateContractorCommand, Contractor>()
                .ForMember(dest => dest.ContractorId, opt => opt.MapFrom(src => src.ContractorId.ToUpper()));
            CreateMap<Contractor, ContractorDto>();

            CreateMap<CreateDocumentItemCommand, DocumentItem>();
            CreateMap<DocumentItem, DocumentItemDto>()
                .ReverseMap();

            CreateMap<CreateDocumentWarehouseUnitItemCommand, DocumentWarehouseUnitItem>();
            CreateMap<DocumentWarehouseUnitItem, DocumentWarehouseUnitItemDto>();

            CreateMap<CreatePzCommand, PZ>();
            CreateMap<PZ, PzDto>();

            CreateMap<CreateWzCommand, WZ>();
            CreateMap<WZ, WzDto>();

            CreateMap<CreateMmmCommand, MMM>();
            CreateMap<MMM, MmmDto>();
            CreateMap<MMM, FlatMmmDto>();

            CreateMap<MMP, MmpDto>();
            CreateMap<MMP, FlatMmpDto>();

            CreateMap<CreatePwCommand, PW>();
            CreateMap<PW, PwDto>();

            CreateMap<CreateRwCommand, RW>();
            CreateMap<RW, RwDto>();

            CreateMap<CreateZwCommand, ZW>()
                .ForMember(dest => dest.DocumentItems, opt => opt.Ignore());
            CreateMap<ZW, ZwDto>();

            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>))
                .ForMember("Items", opt => opt.MapFrom("Items"));

            CreateMap<WarehouseStock, WarehouseStockDto>()
                .ReverseMap();
        }
    }
}
