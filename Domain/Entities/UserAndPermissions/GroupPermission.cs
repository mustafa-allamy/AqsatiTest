using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAndPermissions
{
    public class GroupPermission : BaseEntity<int>
    {
        public int GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public PermissionGroup PermissionGroup { get; set; }

        public int PermissionId { get; set; }
        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; }
    }
}