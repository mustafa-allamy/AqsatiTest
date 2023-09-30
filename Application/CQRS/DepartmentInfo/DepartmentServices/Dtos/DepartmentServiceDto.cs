using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Common.Dto;
using Domain.Entities.Departments;
using Domain.Enums;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Dtos
{
    public class DepartmentServiceDto : BaseDto<DepartmentServiceDto, DepartmentService>
    {
        public GeneralServiceDto? GeneralService { get; set; }

        public string Name { get; set; }
        public double Amount { get; set; }
        public ServiceKind Kind { get; set; }
        public bool IsPercentage { get; set; }
        public ServiceType Type { get; set; }
        public int Priority { get; set; }

        public ICollection<DepartmentServiceDto> ChildServices { get; set; }

    }
}