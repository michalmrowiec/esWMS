namespace esWMS.Application.Functions.Contractors
{
    public class ContractorDto
    {
        public string ContractorId { get; set; } = null!;
        public string ContractorName { get; set; } = null!;
        public string? VatId { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsRecipient { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
