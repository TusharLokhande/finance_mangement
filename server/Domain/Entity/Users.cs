using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Users : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AzureOid { get; set; } = string.Empty;
        public List<TransactionEntity> Transactions { get; set; } = [];
        public ICollection<AccountEntity> BankAccounts { get; set; } = [];
    }
}
