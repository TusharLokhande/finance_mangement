
namespace Domain.Entity
{
    public class AccountEntity : BaseEntity
    {
        public AccountEntity(
                Guid userId,
                string accountNumber,
                string bankName
            )
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            BankName = bankName;
            IsActive = true;
            CreatedAt = DateTimeOffset.UtcNow;
            UserId = userId;
        }
        public Guid UserId { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }

        #region Navigation Properties
        public Users? User { get; set; }
        public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
        #endregion
    }
}