using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentBank : BaseEntity<int>
    {
        public int GeneralBankId { get; set; }
        [ForeignKey(nameof(GeneralBankId))] public GeneralBank GeneralBank { get; set; }



        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

    }
}