using Application.Interfaces.Persistence.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InfraStructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _db;
        private readonly IUserRepository _userRepository;

        public CurrentUserService(IHttpContextAccessor httpContext, AppDbContext db, IUserRepository userRepository)
        {
            _httpContext = httpContext;
            _db = db;
            _userRepository = userRepository;
        }

        public async Task<Guid> GetUserIdAsync()
        {
            var context = _httpContext.HttpContext!;

            // ✅ Check if already cached in this request
            if (context.Items.TryGetValue("CurrentUser", out var cached))
            {
                return ((Users)cached).Id;
            }

            var user = context.User;
            var oid = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            var appUser = await _userRepository.CreateUserIfNotExists(
                user.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown",
                user.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown",
                oid ?? throw new Exception("Azure OID claim not found.")
            );
            context.Items["CurrentUser"] = appUser;
            return appUser.Id;
        }
    }
}
