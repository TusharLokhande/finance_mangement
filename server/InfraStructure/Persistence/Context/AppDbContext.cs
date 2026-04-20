using Application.Features.Expenses.Response;
using Application.Interfaces.Services;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InfraStructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expenses> Expenses => Set<Expenses>();
        public DbSet<ExpenseTagMapping> ExpenseTagMapping => Set<ExpenseTagMapping>();
        public DbSet<Tags> Tags => Set<Tags>();
        public DbSet<Users> Users => Set<Users>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<ExpenseStatsDtoRaw>().HasNoKey().ToView(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
