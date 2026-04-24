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

        public CurrentUserService(IHttpContextAccessor httpContext, AppDbContext db)
        {
            _httpContext = httpContext;
            _db = db;
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
            var appUser = await _db.Users.FirstOrDefaultAsync(x => x.AzureOid == oid);
            context.Items["CurrentUser"] = appUser;
            return appUser.Id;
        }
    }
}
