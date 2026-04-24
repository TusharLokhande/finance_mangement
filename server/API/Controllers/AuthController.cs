using API.Extensions;
using Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _Auth;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public AuthController(
                IAuthService auth,
                IHttpContextAccessor httpContextAccessor
            )
        {
            _Auth = auth;
            _HttpContextAccessor = httpContextAccessor;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
           if(_HttpContextAccessor.HttpContext.User == null) return NotFound();
           var result = await _Auth.InsertUpdateUser();
           return result.ToActionResult();
        }
    }
}
