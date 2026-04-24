using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.RateLimiting;

namespace InfraStructure.Persistence.Configurations
{
    public class ExpenseConfiguration: BaseConfiguration<ExpensesEntity>
    {
        public override void Configure(EntityTypeBuilder<ExpensesEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Expenses");


            builder.Property(k => k.Description).HasMaxLength(500);

            builder.Property(x => x.Amount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Category)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(x => x.Date)
                   .IsRequired();

            builder.HasIndex(x => new
            {
                x.UserId, x.IsActive, 
            });

            builder.HasIndex(x => new
            {
                x.Category, x.Amount, 
            });

            builder.HasOne(k => k.User)
                   .WithMany(k => k.Expenses)
                   .HasForeignKey(k => k.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
