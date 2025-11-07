using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> ResultAsync(RegisterDto registerDto)
            => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));

    }
}
