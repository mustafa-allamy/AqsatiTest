using Domain.Entities.UserAndPermissions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public interface ITokenService
    {
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        public string GenerateRefreshToken();
        public JwtSecurityToken GenerateToken(User user);
        public int GetRefreshTokenExpireDate();
    }
}