using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class ZwRepository(EsWmsDbContext context, ILogger<ZwRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<ZW>(context, logger, sieveProcessor), IZwRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<ZwRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<PagedResult<DocumentItem>> GetEligibleItemsForZwReturn(SieveModel sieveModel)
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);
            var today = DateTime.UtcNow;

            var rwDocuments = await _context.Set<RW>()
                .Include(rw => rw.DocumentItems)
                .Where(rw => rw.DocumentIssueDate >= oneYearAgo && rw.DocumentIssueDate <= today)
                .Where(rw => rw.IsApproved)
                .AsNoTracking()
                .ToListAsync();

            var zwDocuments = await _context.Set<ZW>()
                .Include(zw => zw.DocumentItems)
                .Where(zw => zw.DocumentIssueDate >= oneYearAgo && zw.DocumentIssueDate <= today)
                .AsNoTracking()
                .ToListAsync();

            var rwGroupedItems = rwDocuments
                .SelectMany(doc => doc.DocumentItems)
                .GroupBy(item => new
                {
                    item.ProductId,
                    item.BestBefore,
                    item.BatchLot,
                    item.SerialNumber,
                    item.ProductName,
                    item.ProductCode,
                    item.EanCode
                })
                .ToList();

            var zwGroupedItems = zwDocuments
                .SelectMany(doc => doc.DocumentItems)
                .GroupBy(item => new
                {
                    item.ProductId,
                    item.BestBefore,
                    item.BatchLot,
                    item.SerialNumber,
                    item.ProductName,
                    item.ProductCode,
                    item.EanCode
                })
                .ToList();

            var eligibleItems = new List<DocumentItem>();

            foreach (var rwGroup in rwGroupedItems)
            {
                var matchingZwGroup = zwGroupedItems.FirstOrDefault(g =>
                    AreKeysMatching(g.Key, rwGroup.Key));

                var totalIssuedQuantity = rwGroup.Sum(item => item.Quantity);
                var totalReturnedQuantity = matchingZwGroup?.Sum(item => item.Quantity) ?? 0;

                if (totalIssuedQuantity > totalReturnedQuantity)
                {
                    var remainingQuantity = totalIssuedQuantity - totalReturnedQuantity;
                    var firstItem = rwGroup.First();
                    firstItem.Quantity = remainingQuantity;

                    eligibleItems.Add(firstItem);
                }
            }

            var eligibleItemsQueryable = eligibleItems.AsQueryable();
            var pagedEligibleItems = _sieveProcessor.Apply(sieveModel, eligibleItemsQueryable).ToList();

            var totalCount = eligibleItems.Count;

            return new PagedResult<DocumentItem>(pagedEligibleItems, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }

        private bool AreKeysMatching(dynamic key1, dynamic key2)
        {
            return key1.ProductId == key2.ProductId &&
                   key1.BestBefore == key2.BestBefore &&
                   key1.BatchLot == key2.BatchLot &&
                   key1.SerialNumber == key2.SerialNumber &&
                   key1.ProductName == key2.ProductName &&
                   key1.ProductCode == key2.ProductCode &&
                   key1.EanCode == key2.EanCode;
        }
    }
}
