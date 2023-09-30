using Common.Dto;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos
{
    public class UnitDto : BaseDto<UnitDto, Unit>
    {

        public string Name { get; set; }
        public List<UnitDto> SubUnits { get; set; }
    }
}