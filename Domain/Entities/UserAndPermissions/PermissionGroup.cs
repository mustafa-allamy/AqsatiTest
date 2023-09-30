using Common.Entities;

namespace Domain.Entities.UserAndPermissions
{
    public class PermissionGroup : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}