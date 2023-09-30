using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Common.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetCurrentUserIdFromToken(this HttpContext context)
        {
            var auth = context.Request.Headers["Authorization"].ToString();
            var token = auth.Split(' ')[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(token);
            return Convert.ToInt32(readToken.Payload["sid"]
                .ToString());
        }


        /// <summary>
        /// Use this function only when you store "DepartmentId" in token as Int
        /// </summary>
        /// <param name="context"></param>
        /// <returns>returns TenantId</returns>
        public static int? GetDepartmentId(this HttpContext context)
        {
            var auth = context.Request.Headers["Authorization"].ToString();
            var token = auth.Split(' ')[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(token);
            try
            {
                return Convert.ToInt32(readToken.Payload["DepartmentId"].ToString());
            }
            catch
            {
                return null;
            }
        }
    }
}