using Application.Interfaces;
using Application.Interfaces.Persistence.Repository;
using Application.Interfaces.Services;
using InfraStructure.Extensions;
using InfraStructure.Persistence;
using InfraStructure.Persistence.Context;
using InfraStructure.Repository;
using InfraStructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Diagnostics.Contracts;

namespace InfraStructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(
                 this IServiceCollection services, IConfiguration configuration
            )
        {

            services.AddRateLimiter();


            string ConnectionString = configuration.GetConnectionString("DefaultConnection");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnectionString);
            dataSourceBuilder.ConnectionStringBuilder.Options = "-c timezone=UTC";
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<AppDbContext>(opts =>
                opts.UseNpgsql(dataSource));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
