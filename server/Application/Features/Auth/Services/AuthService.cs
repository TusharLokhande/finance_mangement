using Application.Common;
using Application.Features.Auth.Interfaces;
using Application.Interfaces;
using Application.Interfaces.Persistence.Repository;
using Domain.Entity;
using Microsoft.AspNetCore.Http;


namespace Application.Features.Auth.Services
{
    public class AuthService: IAuthService
    {

        private readonly IUserRepository _userRepository;

        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _uow;
        public AuthService(
                IUserRepository userRepository,
                IHttpContextAccessor _http,
                IUnitOfWork unitOfWork
            )
        {
            _userRepository = userRepository;
            _httpContext = _http;
            _uow = unitOfWork;
        }


        public async Task<Result<Guid?>> InsertUpdateUser()
        {

            var context = _httpContext.HttpContext;
            if (context == null)
                return Result<Guid?>.Failure("No HttpContext", Enums.ErrorStatus.UnAuthorized);


            var user = context.User;

            if (user?.Identity.IsAuthenticated != true)
                return Result<Guid?>.Failure("User not authenticated", Enums.ErrorStatus.UnAuthorized);

            var oid = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            var email = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = user.FindFirst("name")?.Value;

            var appUser = await _userRepository.FirstOrDefaultAsync(x => x.AzureOid == oid);

            if (appUser == null)
            {
                appUser = new Users
                {
                    Id = Guid.NewGuid(),
                    AzureOid = oid!,
                    Email = email,
                    Name = name
                };
                await _userRepository.AddAsync(appUser);
                await _uow.SaveChangesAsync();
            }
            context.Items["CurrentUser"] = appUser;
            return Result<Guid?>.Success(appUser.Id);
        }
    }
}
