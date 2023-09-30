using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAndPermissions
{
    public class UserPermission : BaseEntity<int>
    {
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; }

        public int PermissionId { get; set; }
    }
}