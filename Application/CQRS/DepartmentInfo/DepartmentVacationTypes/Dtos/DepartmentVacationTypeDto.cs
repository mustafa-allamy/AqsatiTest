using Common.Dto;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos
{
    public class DepartmentVacationTypeDto : BaseDto<DepartmentVacationTypeDto, DepartmentVacationType>
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool EffectedByAllAllowances { get; set; }
        public bool EffectedByAllDeductions { get; set; }

        public List<DepartmentVacationServiceRule> VacationServiceRules { get; set; }
    }
}