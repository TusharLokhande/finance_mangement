using Application.Features.Auth.Interfaces;
using Application.Features.Auth.Services;
using Application.Features.Bank.Interfaces;
using Application.Features.Bank.Services;
using Application.Features.Transactions.Intefaces;
using Application.Features.Transactions.Services;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Mapster
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            #endregion

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBankService, BankService>();

            return services;
        }
    }
}
