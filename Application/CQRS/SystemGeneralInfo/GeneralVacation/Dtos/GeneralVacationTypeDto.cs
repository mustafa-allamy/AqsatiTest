using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos
{
    public class GeneralVacationTypeDto : BaseDto<GeneralVacationTypeDto, GeneralVacationType>
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool EffectedByAllAllowances { get; set; }
        public bool EffectedByAllDeductions { get; set; }

        public List<GeneralVacationServiceRuleDto> VacationServiceRules { get; set; }
    }
}