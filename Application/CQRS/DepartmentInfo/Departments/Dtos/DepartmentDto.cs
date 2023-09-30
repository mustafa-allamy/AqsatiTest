using Common.Dto;
using Domain.Entities.Departments;
using Domain.Entities.MinistryAndGov;

namespace Application.CQRS.DepartmentInfo.Departments.Dtos
{
    public class DepartmentDto : BaseDto<DepartmentDto, Department>
    {
        public Ministry Ministry { get; set; }

        public string Name { get; set; }
        public string Domain { get; set; }

        public DepartmentSetting DepartmentSetting { get; set; }
        public DepartmentReportSetting DepartmentReportSetting { get; set; }
    }
}