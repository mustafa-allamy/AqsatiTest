using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAndPermissions
{
    public class UserPermissionGroup : BaseEntity<int>
    {
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        public int PermissionGroupId { get; set; }

        [ForeignKey(nameof(PermissionGroupId))]
        public PermissionGroup PermissionGroup { get; set; }

    }
}