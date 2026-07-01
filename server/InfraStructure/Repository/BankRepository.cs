using Application.Interfaces.Persistence.Repository;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository
{
    public class BankRepository : GenericRepository<AccountEntity>, IBankRepository
    {
        private readonly AppDbContext _db;

        public BankRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AccountEntity>> GetUserBanksAsync(Guid userId)
        {
            return await _db.BankAccounts
                .Where(b => b.UserId == userId && b.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AccountEntity?> GetUserBankByIdAsync(Guid userId, Guid bankId)
        {
            return await _db.BankAccounts
                .Where(b => b.UserId == userId && b.Id == bankId && b.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
