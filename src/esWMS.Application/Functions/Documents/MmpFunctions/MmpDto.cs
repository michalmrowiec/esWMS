﻿using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Warehouses;

namespace esWMS.Application.Functions.Documents.MmpFunctions
{
    public class MmpDto : CreateBaseDocumentCommand
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string FromWarehouseId { get; set; } = null!;

        public WarehouseDto? FromWarehouse { get; set; }
    }
}