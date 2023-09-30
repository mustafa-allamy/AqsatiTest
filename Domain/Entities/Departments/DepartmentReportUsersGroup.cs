using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentReportUsersGroup : BaseEntity<int>
    {
        public string GroupName { get; set; }
        public ICollection<DepartmentReportUser> ReportUsers { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}