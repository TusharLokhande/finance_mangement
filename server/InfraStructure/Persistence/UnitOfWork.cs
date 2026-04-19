using Application.Interfaces;
using InfraStructure.Persistence.Context;

namespace InfraStructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            var hasTransaction = _context.Database.CurrentTransaction != null;

            var result = await _context.SaveChangesAsync();

            if (hasTransaction)
            {
                await _context.Database.CurrentTransaction.CommitAsync();
            }

            return result;
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
