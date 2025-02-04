﻿using esWMS.Application.Responses;
using MediatR;
using System.ComponentModel;

namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public class CreateDocumentItemCommand : IRequest<BaseResponse<DocumentItemDto>>
    {
        public string? DocumentId { get; set; }
        public string ProductId { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime? BestBefore { get; set; }
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        public string? SerialNumber { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? VatRate { get; set; }
        public string? Unit { get; set; }
        public bool IsApproved { get; set; }
        public string? CreatedBy { get; set; }

        public List<CreateDocumentWarehouseUnitItemCommand> DocumentWarehouseUnitItems { get; set; } = [];
    }
}
