using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Persistence.Configurations
{
    internal class TransactionTagMappingConfiguration : BaseConfiguration<TransactionTagMapping>
    {
        public override void Configure(EntityTypeBuilder<TransactionTagMapping> builder)
        {
            base.Configure(builder);
            builder.ToTable("TransactionTagMapping");

            builder.HasKey(k => new { k.TagId, k.TransactionId });

            builder.HasIndex(k => k.TagId);
            builder.HasIndex(k => k.TransactionId);

            builder.HasOne(etm => etm.Tags)
                .WithMany(t => t.TransactionTagMapping)
                .HasForeignKey(etm => etm.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(etm => etm.Transaction)
                .WithMany(t => t.TransactionsTagMappings)
                .HasForeignKey(etm => etm.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
