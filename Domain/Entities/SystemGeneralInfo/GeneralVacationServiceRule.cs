using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralVacationServiceRule : BaseEntity<int>
    {
        public int VacationTypeId { get; set; }
        [ForeignKey(nameof(VacationTypeId))]
        public GeneralVacationType VacationType { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public GeneralService Service { get; set; }
        public bool NotEffectedByBasicSalaryDeduction { get; set; }

        public int Amount { get; set; }
    }
}