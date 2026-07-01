using Domain.Entity;

namespace Application.Interfaces.Persistence.Repository
{
    public interface IBankRepository : IGenericRepository<AccountEntity>
    {
        Task<IEnumerable<AccountEntity>> GetUserBanksAsync(Guid userId);
        Task<AccountEntity?> GetUserBankByIdAsync(Guid userId, Guid bankId);
    }
}
