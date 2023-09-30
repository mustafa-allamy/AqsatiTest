using Common.Dto;
using Domain.Entities.UserAndPermissions;

namespace Application.CQRS.Permission.Dtos
{
    public class GroupPermissionDto : BaseDto<GroupPermissionDto, GroupPermission>
    {
        public PermissionDto Permission { get; set; }
    }
}