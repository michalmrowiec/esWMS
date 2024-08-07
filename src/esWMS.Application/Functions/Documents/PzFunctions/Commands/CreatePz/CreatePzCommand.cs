﻿using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    public class CreatePzCommand
        : CreateBaseDocumentCommand, IRequest<BaseResponse<PzDto>>
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;
    }
}
