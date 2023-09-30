using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Dto;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos
{
    public class DepartmentVacationServiceRuleDto : BaseDto<DepartmentVacationServiceRuleDto, DepartmentVacationServiceRule>
    {
        public DepartmentServiceDto Service { get; set; }
        public bool NotEffectedByBasicSalaryDeduction { get; set; }

        public int Amount { get; set; }
    }
}