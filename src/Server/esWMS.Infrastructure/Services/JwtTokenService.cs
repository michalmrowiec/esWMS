using esWMS.Application.Contracts.Services;
using esWMS.Domain.Entities.SystemActors;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace esWMS.Infrastructure.Services
{
    internal class JwtTokenService
        (AuthenticationSettings authenticationSettings)
        : IJwtTokenService
    {
        private readonly AuthenticationSettings _authenticationSettings = authenticationSettings;
        public string GenerateJwt(Employee employee)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                new(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
                new(ClaimTypes.Role, $"{employee.RoleId}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiress = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDaysForNormalLogin);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expiress,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
