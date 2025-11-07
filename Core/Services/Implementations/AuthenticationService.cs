using Domain.Entities.IdentityModule;
using Domain.Exepctions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new UnauthorizedExecption();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new UnauthorizedExecption();
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user) , user.Email);
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
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationExecption(errors);
            }
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user) , user.Email);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,user.DisplayName),
                new Claim(ClaimTypes.Email ,user.Email),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cc1c1df4d6e737eaee2b95ae00eac97bb62cd13c962572299b2287e960bca7735cd4a6232273013414a3bdce7bd81161692453dda8312a4d08f4e1b831cc667c"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: "https://localhost:7069"
                ,audience:"AngularProject"
                ,claims:claims
                ,expires:DateTime.UtcNow.AddDays(30)
                ,signingCredentials:signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
