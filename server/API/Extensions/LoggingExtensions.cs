using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Grafana.Loki;

namespace API.Extensions;

public static class LoggingExtensions
{
    public static IHostBuilder AddSerilogLogging(
        this IHostBuilder hostBuilder,
        IConfiguration configuration)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "FinanceManager")
                .Enrich.WithProperty(
                    "Environment",
                    configuration["ASPNETCORE_ENVIRONMENT"]
                )
                .Enrich.WithEnvironmentName()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()

                .WriteTo.GrafanaLoki(
                    "http://loki:3100",
                    labels: new[]
                    {
                        new LokiLabel
                        {
                            Key = "service",
                            Value = "finance-manager-api"
                        }
                    });
        });

        return hostBuilder;
    }
}