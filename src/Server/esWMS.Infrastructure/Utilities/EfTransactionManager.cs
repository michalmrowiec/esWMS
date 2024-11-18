using esWMS.Application.Contracts.Utilities;
using Microsoft.EntityFrameworkCore.Storage;

namespace esWMS.Infrastructure.Utilities
{
    internal class EfTransactionManager(EsWmsDbContext context) : ITransactionManager
    {
        private readonly EsWmsDbContext _context = context;
        private IDbContextTransaction? _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
    }
}