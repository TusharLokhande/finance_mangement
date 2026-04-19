using Application.Common;
using Application.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(ApiResponse<T>.FromResult(result));

            return result.ErrorStatus switch
            {
                ErrorStatus.ValidationError => new BadRequestObjectResult(ApiResponse<T>.FromResult(result)),
                ErrorStatus.Duplicate => new ConflictObjectResult(ApiResponse<T>.FromResult(result)),
                ErrorStatus.NotFound => new NotFoundObjectResult(ApiResponse<T>.FromResult(result)),


                ErrorStatus.UnAuthorized => new UnauthorizedObjectResult(ApiResponse<T>.FromResult(result)),
                ErrorStatus.InvalidCred => new UnauthorizedObjectResult(ApiResponse<T>.FromResult(result)),
                ErrorStatus.TokenExpired => new UnauthorizedObjectResult(ApiResponse<T>.FromResult(result)),

                ErrorStatus.InternalServerError => new ObjectResult(ApiResponse<T>.FromResult(result)) { StatusCode = 500 },
                _ => new ObjectResult(ApiResponse<T>.FromResult(result)) { StatusCode = 500 }
            };
        }
    }
}
