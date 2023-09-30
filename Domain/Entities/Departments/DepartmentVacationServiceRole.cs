using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentVacationServiceRule : BaseEntity<int>
    {
        public int VacationTypeId { get; set; }
        [ForeignKey(nameof(VacationTypeId))]
        public DepartmentVacationType VacationType { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public DepartmentService Service { get; set; }
        public bool NotEffectedByBasicSalaryDeduction { get; set; }

        public int Amount { get; set; }
    }
}