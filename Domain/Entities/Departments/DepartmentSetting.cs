using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentSetting : BaseEntity<int>
    {
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))] public Department Department { get; set; }


    }
}