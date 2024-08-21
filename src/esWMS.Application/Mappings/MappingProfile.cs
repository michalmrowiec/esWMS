﻿using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Contractors.Commands.CreateContractor;
using esWMS.Application.Functions.Documents.BaseDocumentFunctions;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.UpdateDocumentItems;
using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm;
using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Functions.Documents.MmpFunctions.Commands.CreateMmp;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Functions.Documents.WzFunctions;
using esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz;
using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.Locations.Commands.CreateLocation;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Functions.Warehouses;
using esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse;
using esWMS.Application.Functions.WarehouseUnitItems;
using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;
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
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<Contractor, ContractorDto>();

            CreateMap<CreateWarehouseCommand, Warehouse>();
            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<CreateWarehouseUnitCommand, WarehouseUnit>();
            CreateMap<WarehouseUnit, WarehouseUnitDto>()
                .ForMember(dto => dto.WarehouseUnitItems,
                           opt => opt.MapFrom(src => src.WarehouseUnitItems));

            CreateMap<WarehouseUnit, FlatWarehouseUnitDto>();

            CreateMap<CreateWarehouseUnitItemCommand, WarehouseUnitItem>();
            CreateMap<WarehouseUnitItem, WarehouseUnitItemDto>();
            CreateMap<WarehouseUnitItem, DocumentItem>()
                .ForMember(di => di.ProductId, opt => opt.MapFrom(wui => wui.ProductId))
                .ForMember(di => di.ProductCode, opt => opt.MapFrom(wui => wui.Product.ProductCode))
                .ForMember(di => di.EanCode, opt => opt.MapFrom(wui => wui.Product.EanCode))
                .ForMember(di => di.Quantity, opt => opt.MapFrom(wui => wui.Quantity))
                .ForMember(di => di.BestBefore, opt => opt.MapFrom(wui => wui.BestBefore))
                .ForMember(di => di.BatchLot, opt => opt.MapFrom(wui => wui.BatchLot))
                .ForMember(di => di.SerialNumber, opt => opt.MapFrom(wui => wui.SerialNumber))
                .ForMember(di => di.Price, opt => opt.MapFrom(wui => wui.Price))
                .ForMember(di => di.Currency, opt => opt.MapFrom(wui => wui.Currency));

            CreateMap<CreateZoneCommand, Zone>();
            CreateMap<Zone, ZoneDto>();

            CreateMap<CreateLocationCommand, Location>();
            CreateMap<Location, LocationDto>();

            CreateMap<CreateContractorCommand, Contractor>();
            CreateMap<Contractor, ContractorDto>();

            CreateMap<CreateDocumentItemCommand, DocumentItem>();
            CreateMap<DocumentItem, DocumentItemDto>();

            CreateMap<CreateDocumentWarehouseUnitItemCommand, DocumentWarehouseUnitItem>();
            CreateMap<DocumentWarehouseUnitItem, DocumentWarehouseUnitItemDto>();

            CreateMap<CreatePzCommand, PZ>();
            CreateMap<PZ, PzDto>();

            CreateMap<CreateWzCommand, WZ>();
            CreateMap<WZ, WzDto>();

            CreateMap<CreateMmmCommand, MMM>();
            CreateMap<MMM, MmmDto>();

            CreateMap<CreateMmpCommand, MMP>();
            CreateMap<MMP, MmpDto>();

            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>))
                .ForMember("Items", opt => opt.MapFrom("Items"));

            CreateMap<WarehouseStock, WarehouseStockDto>()
                .ReverseMap();
        }
    }
}
