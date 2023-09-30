using Common.Entities;
using Domain.Entities.UserAndPermissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentReportUser : BaseEntity<int>
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))] public User User { get; set; }
        public int GroupId { get; set; }
        [ForeignKey(nameof(GroupId))] public DepartmentReportUsersGroup Group { get; set; }
    }
}