using API.Extensions;
using API.Middlewares;
using Application;
using Application.Features.Expenses.Request;
using FluentValidation;
using InfraStructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Scalar.AspNetCore;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddCorsPolicy(builder.Configuration)
    .AddApplication();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

builder.Services.AddControllers(
    (option) => option.Filters.Add<FluentValidatorFilter>()
);

builder.Services.Scan(scan =>
    scan.FromAssemblyOf<ExpenseDto>()
    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
    .AsImplementedInterfaces()
    .WithSingletonLifetime()
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "My API";
        options.Theme = ScalarTheme.Kepler;
    });
}
app.UseCors("DefaultCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseGlobalExpectionMiddleware();
app.MapControllers();
app.Run();
