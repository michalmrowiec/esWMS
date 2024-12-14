using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.PurgeEmptyWarehouseUnitsAndItems;

public record PurgeEmptyWarehouseUnitsAndItemsCommand : IRequest<BaseResponse>;
