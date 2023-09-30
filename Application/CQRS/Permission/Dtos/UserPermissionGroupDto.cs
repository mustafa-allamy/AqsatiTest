using Common.Dto;
using Domain.Entities.UserAndPermissions;

namespace Application.CQRS.Permission.Dtos
{
    public class UserPermissionGroupDto : BaseDto<UserPermissionGroupDto, UserPermissionGroup>
    {
        public PermissionGroupDto PermissionGroup { get; set; }
    }
}