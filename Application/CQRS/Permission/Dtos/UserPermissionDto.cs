using Common.Dto;
using Domain.Entities.UserAndPermissions;

namespace Application.CQRS.Permission.Dtos
{
    public class UserPermissionDto : BaseDto<UserPermissionDto, UserPermission>
    {
        public PermissionDto Permission { get; set; }
    }
}