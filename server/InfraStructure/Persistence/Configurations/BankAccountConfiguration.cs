
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Persistence.Configurations;

public class BankAccountConfiguration : BaseConfiguration<AccountEntity>
{
    public override void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("BankAccounts");

        builder.Property(x => x.AccountNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.BankName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.IsActive,
        });

        builder.HasOne(k => k.User)
               .WithMany(k => k.BankAccounts)
               .HasForeignKey(k => k.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}