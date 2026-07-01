namespace Domain.Entity
{
    public class TransactionTagMapping : BaseEntity
    {
        public TransactionTagMapping(
                Guid TagId,
                Guid TransactionId
            )
        {
            this.TagId = TagId;
            this.TransactionId = TransactionId;
            this.CreatedAt = DateTimeOffset.UtcNow;
            this.Id = Guid.NewGuid();
        }

        public TransactionTagMapping()
        {
        }

        public Guid TagId { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionEntity Transaction { get; set; }
        public Tags Tags { get; set; }
    }
}
