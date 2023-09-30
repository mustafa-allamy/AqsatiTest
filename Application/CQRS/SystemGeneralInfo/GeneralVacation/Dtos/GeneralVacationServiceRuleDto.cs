using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos
{
    public class GeneralVacationServiceRuleDto : BaseDto<GeneralVacationServiceRuleDto, GeneralVacationServiceRule>
    {
        public GeneralServiceDto Service { get; set; }
        public bool NotEffectedByBasicSalaryDeduction { get; set; }

        public int Amount { get; set; }
    }
}