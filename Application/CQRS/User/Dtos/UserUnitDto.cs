using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Common.Dto;
using Domain.Entities.UserAndPermissions;

namespace Application.CQRS.User.Dtos
{
    public class UserUnitDto : BaseDto<UserUnitDto, UserUnit>
    {
        public UnitDto Unit { get; set; }
    }
}