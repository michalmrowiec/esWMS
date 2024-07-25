using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<BaseResponse<ProductDto>>
    {
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public int? WeightPerUnit { get; set; }
        public int? StorageTemperature { get; set; }
        public bool IsMedia { get; set; }
        public string? MediaTypeAlias { get; set; }
        public decimal? Price { get; set; }
        public string? SupplierContractorId { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
    }
}
