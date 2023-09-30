using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class Unit : BaseEntity<int>
    {
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Unit ParentUnit { get; set; }
        public string Name { get; set; }
        public ICollection<Unit> SubUnits { get; set; }
    }
}