using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;


namespace InfraStructure.Extensions
{
    public static class RateLimitExtension
    {
        public static IServiceCollection AddRateLimiter(
            this IServiceCollection services
        )
        {

            // RATE LIMITTER
            services.AddRateLimiter(options =>
            {
                //  IP-based global limit
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,              
                        Window = TimeSpan.FromMinutes(1), 
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    });
                });

                // Controller-based limit (named policy)
                options.AddFixedWindowLimiter("STRICT", o =>
                {
                    o.PermitLimit = 10;              
                    o.Window = TimeSpan.FromSeconds(30); 
                    o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    o.QueueLimit = 0;
                });
            });


            return services;
        }
    }
}
