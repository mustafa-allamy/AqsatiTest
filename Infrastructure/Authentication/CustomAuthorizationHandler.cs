using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Authentication
{
    public class CustomAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CustomAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            string? stringUserId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)?.Value;

            if (!int.TryParse(stringUserId, out var userId))
            {
                return;
            }
            if (requirement.Permission == "Auth")
                context.Succeed(requirement);
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = await permissionService.GetPermissionsAsync(userId);


            if (permissions.Contains(requirement.Permission))
                context.Succeed(requirement);
        }
    }
}