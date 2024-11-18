using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;

namespace esWMS.Application.Services
{
    internal static class DocumentWarehouseTransactionService
    {
        internal static async Task<BaseResponse<TDto>> CommitChangesAsync<T, TDto>
            (T document,
            IEnumerable<WarehouseUnit> warehouseUnits, 
            ITransactionManager transactionManager,
            IWarehouseUnitRepository warehouseUnitRepository,
            IBaseDocumentRepository<T> documentRepository,
            IMapper mapper)
            where T : BaseDocument
            where TDto : class
        {
            await transactionManager.BeginTransactionAsync();

            var updatedWarehouseUnits = await warehouseUnitRepository.UpdateWarehouseUnitsAsync(warehouseUnits.ToArray());
            var updatedDocument = await documentRepository.UpdateAsync(document);

            await transactionManager.CommitTransactionAsync();

            var mappedUpdatedDocument = mapper.Map<TDto>(updatedDocument);
            return new BaseResponse<TDto>(mappedUpdatedDocument);
        }
    }
}
