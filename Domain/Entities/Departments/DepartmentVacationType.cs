using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentVacationType : BaseEntity<int>
    {
        public int? GeneralVacationTypeId { get; set; }
        [ForeignKey(nameof(GeneralVacationTypeId))]
        public GeneralVacationType? GeneralVacationType { get; set; }


        public string Name { get; set; }
        public int Amount { get; set; }
        public bool EffectedByAllAllowances { get; set; }
        public bool EffectedByAllDeductions { get; set; }

        public ICollection<DepartmentVacationServiceRule> VacationServiceRules { get; set; }



        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

    }
}