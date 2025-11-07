using Domain.Entities.IdentityModule;
using Domain.Exepctions;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;

namespace Services.Implementations
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user == null)
                throw new UnauthorizedExecption();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!result)
                throw new UnauthorizedExecption();
            return new UserResultDto(user.DisplayName, "token", user.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) { 
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationExecption(errors);
            }
            return new UserResultDto(user.DisplayName, "token", user.Email);
        }
    }
}
