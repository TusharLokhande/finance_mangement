using API.Extensions;
using Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropodownController : ControllerBase
    {
        private readonly IAuthService _Auth;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public DropodownController(
                IAuthService auth,
                IHttpContextAccessor httpContextAccessor
            )
        {
            _Auth = auth;
            _HttpContextAccessor = httpContextAccessor;
        }


    }
}
