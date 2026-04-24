using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Domain.Entity
{
    public class ExpensesEntity: BaseEntity
    {
        private ExpensesEntity() { } 
        public ExpensesEntity(
                decimal amt,
                CategoryEnum category,
                DateTimeOffset date,
                string desc,
                PaymentTypeEnum payment,
                Guid userId
            )
        {
            Id = Guid.NewGuid();
            Amount = amt;
            Category = category;
            Description = desc;
            Date = date;
            CreatedAt = DateTimeOffset.UtcNow;
            Payment= payment;
            UserId = userId;
        }

        public decimal Amount { get; set; }
        public CategoryEnum Category { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public PaymentTypeEnum Payment { get; set; }
        public Guid UserId { get; set; }

        #region Navigation Properties 
        public ICollection<ExpenseTagMapping> ExpenseTagMapping { get; set; } = new List<ExpenseTagMapping>();

        public Users User { get; set; }

        #endregion

        /// <summary>
        /// INSERT / UPDATE THE TAGS
        /// </summary>
        /// <param name="tagIds"></param>
        public void AddTags(List<Guid> tagIds)
        {
            // 1. Remove mappings that are no longer present
            var toRemove = ExpenseTagMapping
                .Where(x => !tagIds.Contains(x.TagId))
                .ToList();

            foreach (var item in toRemove)
            {
                ExpenseTagMapping.Remove(item);
            }

            // 2. Add only new mappings (avoid duplicates)
            var existingTagIds = ExpenseTagMapping
                .Select(x => x.TagId)
                .ToHashSet();

            foreach (var tagId in tagIds)
            {
                if (existingTagIds.Contains(tagId))
                    continue;
                ExpenseTagMapping.Add(new ExpenseTagMapping(tagId, this.Id));
            }
        }

        public void Modify(
            decimal amt,
            CategoryEnum category,
            DateTimeOffset date,
            string desc,
            PaymentTypeEnum payment
        )
        {
            Amount = amt;
            Category = category;
            Description = desc;
            Date = date;
            Payment = payment;
            ModifiedAt = DateTimeOffset.UtcNow;
        }
        
        public void Delete()
        {
            this.IsActive = false;
            this.ModifiedAt = DateTimeOffset.UtcNow;
            ExpenseTagMapping.Clear();
        }
    }
}
