using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDbContext _dbContext;

        public PermissionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HashSet<string>> GetPermissionsAsync(int userId)
        {
            var permissions = await _dbContext.UserPermissions.Where(x => x.UserId == userId)
                .Select(x => x.Permission.PolicyName)
                .ToArrayAsync();

            return permissions.ToHashSet();
        }
    }
}