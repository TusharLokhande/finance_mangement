using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.RateLimiting;

namespace InfraStructure.Persistence.Configurations
{
       public class TransactionConfiguration : BaseConfiguration<TransactionEntity>
       {
              public override void Configure(EntityTypeBuilder<TransactionEntity> builder)
              {
                     base.Configure(builder);

                     builder.ToTable("Transactions");


                     builder.Property(k => k.Description).HasMaxLength(500);

                     builder.Property(x => x.Amount)
                             .IsRequired()
                             .HasColumnType("decimal(18,2)");

                     builder.Property(x => x.Category)
                            .IsRequired()
                            .HasConversion<int>();

                     builder.Property(x => x.Date)
                            .IsRequired();

                     builder.Property(x => x.TransactionType)
                            .IsRequired()
                            .HasConversion<int>();

                     builder.Property(x => x.LentStatus)
                            .IsRequired()
                            .HasConversion<int>();

                     builder.HasIndex(x => new
                     {
                            x.UserId,
                            x.IsActive,
                     });

                     builder.HasIndex(x => new
                     {
                            x.Category,
                            x.Amount,
                     });

                     builder.HasOne(k => k.User)
                            .WithMany(k => k.Transactions)
                            .HasForeignKey(k => k.UserId)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasOne(k => k.BankAccountEntity)
                            .WithMany(k => k.Transactions)
                            .HasForeignKey(k => k.BankAccount)
                            .OnDelete(DeleteBehavior.Restrict);
              }
       }
}
