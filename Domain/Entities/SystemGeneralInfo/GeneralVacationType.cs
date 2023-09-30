using Common.Entities;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralVacationType : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool EffectedByAllAllowances { get; set; }
        public bool EffectedByAllDeductions { get; set; }

        public ICollection<GeneralVacationServiceRule> VacationServiceRules { get; set; }
    }
}