using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Persistence.Configurations
{
    internal class ExpenseTagMappingConfiguration : BaseConfiguration<ExpenseTagMapping>
    {
        public override void Configure(EntityTypeBuilder<ExpenseTagMapping> builder)
        {
            base.Configure(builder);
            builder.ToTable("ExpenseTagMapping");

            builder.HasKey(k => new { k.TagId, k.ExpenseId });

            builder.HasIndex(k => k.TagId);
            builder.HasIndex(k => k.ExpenseId);

            builder.HasOne(etm => etm.Tags)
                .WithMany(t => t.ExpenseTagMapping)
                .HasForeignKey(etm => etm.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(etm => etm.Expense)
                .WithMany(t => t.ExpenseTagMapping)
                .HasForeignKey(etm => etm.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
