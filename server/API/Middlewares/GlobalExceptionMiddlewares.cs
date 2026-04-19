using API.Extensions;
using Application.Common;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace API.Middlewares
{
    public class GlobalExceptionMiddlewares
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<GlobalExceptionMiddlewares> _logger;

        public GlobalExceptionMiddlewares(
                RequestDelegate next,
                ILogger<GlobalExceptionMiddlewares> logger
            )
        {
            _logger = logger;
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnHandled execption occured");

                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");
                string message =
                          ex.Message;

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var result = ApiResponse<string>.FromResult(Result<string>.Failure(message, Application.Enums.ErrorStatus.InternalServerError));
                await httpContext.Response.WriteAsJsonAsync(result);
            }
        }
    }


    public static class GlobalExpectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExpectionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddlewares>();
        }
    }
}
