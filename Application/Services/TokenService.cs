using Domain.Entities.UserAndPermissions;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                    ValidateLifetime = false,

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal =
                    tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                    return null;

                return principal;
            }
            catch
            {

                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public JwtSecurityToken GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(type: JwtRegisteredClaimNames.Sid, value: user!.Id.ToString()),
                new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Id.ToString()),
                new Claim(type: JwtRegisteredClaimNames.Name, value: user.Email),
                new Claim(type: JwtRegisteredClaimNames.GivenName,
                    value: user.FullName),
                //new Claim(ClaimTypes.Role, user.Role.GetDisplayName()),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                user.DepartmentId!=null?new Claim(type: "DepartmentId", value: user.DepartmentId.Value.ToString()):null,
            };
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(value: int.Parse(_configuration["JWT:Expire"])),
                notBefore: DateTime.UtcNow,
                audience: "Audience",
                issuer: "Issuer",
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding
                        .UTF8
                        .GetBytes(_configuration["JWT:Secret"])),
                    SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public int GetRefreshTokenExpireDate()
        {
            return int.Parse(_configuration["JWT:RefreshTokenExpire"]!);
        }
    }
}