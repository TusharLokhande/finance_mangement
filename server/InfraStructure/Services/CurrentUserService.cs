using Application.Common;
using Application.Interfaces.Services;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
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
            var oid = user.FindFirstValue("oid");

            var appUser = await _db.Users.FirstOrDefaultAsync(x => x.AzureOid == oid);

            if (appUser == null)
            {
                appUser = new Users
                {
                    Id = Guid.NewGuid(),
                    AzureOid = oid!,
                    Email = user.FindFirstValue("preferred_username")!,
                    Name = user.FindFirstValue("name")!
                };
                _db.Users.Add(appUser);
                await _db.SaveChangesAsync();
            }
            context.Items["CurrentUser"] = appUser;
            return appUser.Id;
        }
    }
}
