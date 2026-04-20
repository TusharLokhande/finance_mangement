using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;


        public UnitOfWork(
            AppDbContext context,
            ICurrentUserService currentUserService
        )
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            await ApplyAuditInformation();
            var hasTransaction = _context.Database.CurrentTransaction != null;

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (hasTransaction)
            {
                await _context.Database.CurrentTransaction.CommitAsync(cancellationToken);
            }
            return result;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.RollbackTransactionAsync(cancellationToken);
        }

        private async Task ApplyAuditInformation()
        {
            var entries = _context.ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                var userId = await _currentUserService.GetUserIdAsync();

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    var hasRealChanges = entry.Properties.Any(
                            p => p.IsModified &&
                            p.Metadata.Name != nameof(BaseEntity.ModifiedAt)  &&
                            p.Metadata.Name != nameof(BaseEntity.ModifiedBy)
                        );

                    if (!hasRealChanges) continue;

                    entry.Entity.ModifiedAt= DateTimeOffset.UtcNow;
                    entry.Entity.ModifiedBy= userId;
                    entry.Property(nameof(BaseEntity.ModifiedBy)).IsModified = true;
                    entry.Property(nameof(BaseEntity.ModifiedAt)).IsModified = true;
                }
            }
        }
    }


}
