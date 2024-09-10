using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<BaseResponse<ProductDto>>
    {
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string ShortProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public decimal? MinStorageTemperature { get; set; }
        public decimal? MaxStorageTemperature { get; set; }
        public bool IsMedia { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? VatRate { get; set; }
        public string? SupplierContractorId { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
    }
}
