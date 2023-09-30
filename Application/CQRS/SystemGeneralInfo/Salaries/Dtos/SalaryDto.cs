using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.Salaries.Dtos
{
    public class SalaryDto : BaseDto<SalaryDto, Salary>
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public int Level { get; set; }
        public List<GradeDto> Grades { get; set; }
    }
}