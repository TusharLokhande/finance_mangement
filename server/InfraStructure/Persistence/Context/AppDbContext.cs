using Application.Features.Transactions.Response;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();
        public DbSet<TransactionTagMapping> TransactionTagMapping => Set<TransactionTagMapping>();
        public DbSet<Tags> Tags => Set<Tags>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<AccountEntity> BankAccounts => Set<AccountEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<TransactionStatsDtoRaw>().HasNoKey().ToView(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
