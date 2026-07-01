using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Domain.Entity
{
    public class TransactionEntity : BaseEntity
    {
        private TransactionEntity() { }
        public TransactionEntity(
                decimal amt,
                CategoryEnum category,
                DateTimeOffset date,
                string desc,
                TransactionType transactionType,
                Guid bankAccount,
                string? lentTo,
                LentStatus? lentStatus,
                Guid userId
            )
        {
            Id = Guid.NewGuid();
            Amount = amt;
            Category = category;
            Description = desc;
            Date = date;
            CreatedAt = DateTimeOffset.UtcNow;
            TransactionType = transactionType;
            BankAccount = bankAccount;
            UserId = userId;
        }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public CategoryEnum Category { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid BankAccount { get; set; }
        public string? LentTo { get; set; }
        public LentStatus? LentStatus { get; set; }
        public Guid UserId { get; set; }

        #region Navigation Properties 
        public ICollection<TransactionTagMapping> TransactionsTagMappings { get; set; } = new List<TransactionTagMapping>();

        public Users User { get; set; }
        public AccountEntity BankAccountEntity { get; set; }

        #endregion

        /// <summary>
        /// INSERT / UPDATE THE TAGS
        /// </summary>
        /// <param name="tagIds"></param>
        public void AddTags(List<Guid> tagIds)
        {
            // 1. Remove mappings that are no longer present
            var toRemove = TransactionsTagMappings
                .Where(x => !tagIds.Contains(x.TagId))
                .ToList();

            foreach (var item in toRemove)
            {
                TransactionsTagMappings.Remove(item);
            }

            // 2. Add only new mappings (avoid duplicates)
            var existingTagIds = TransactionsTagMappings
                .Select(x => x.TagId)
                .ToHashSet();

            foreach (var tagId in tagIds)
            {
                if (existingTagIds.Contains(tagId))
                    continue;
                TransactionsTagMappings.Add(new TransactionTagMapping(tagId, this.Id));
            }
        }

        public void Modify(
            decimal amt,
            CategoryEnum category,
            DateTimeOffset date,
            string desc,
            TransactionType transactionType,
            Guid bankAccount,
            string? lentTo,
            LentStatus? lentStatus
        )
        {
            Amount = amt;
            Category = category;
            Description = desc;
            Date = date;
            ModifiedAt = DateTimeOffset.UtcNow;
            TransactionType = transactionType;
            BankAccount = bankAccount;
            LentTo = lentTo;
            LentStatus = lentStatus;
        }

        public void Delete()
        {
            this.IsActive = false;
            this.ModifiedAt = DateTimeOffset.UtcNow;
            TransactionsTagMappings.Clear();
        }
    }
}
