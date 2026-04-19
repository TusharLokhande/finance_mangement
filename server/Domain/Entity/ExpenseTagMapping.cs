namespace Domain.Entity
{
    public class ExpenseTagMapping : BaseEntity
    {
        public ExpenseTagMapping(
                Guid TagId,
                Guid ExpenseId
            )
        {
            this.TagId = TagId;
            this.ExpenseId = ExpenseId;
            this.CreatedAt = DateTimeOffset.UtcNow;
            this.Id = Guid.NewGuid();
        }

        public ExpenseTagMapping()
        {
        }

        public Guid TagId { get; set; }
        public Guid ExpenseId { get; set; }
        public Expenses Expense { get; set; }
        public Tags Tags { get; set; }
    }
}
