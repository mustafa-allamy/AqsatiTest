using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission, string givenName) : base(policy: permission)
        {
            Permission = permission;
            GivenName = givenName;
        }

        public string Permission { get; }
        public string GivenName { get; }
    }
}