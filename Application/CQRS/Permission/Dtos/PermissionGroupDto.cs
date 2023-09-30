using Common.Dto;
using Domain.Entities.UserAndPermissions;

namespace Application.CQRS.Permission.Dtos
{
    public class PermissionGroupDto : BaseDto<PermissionGroupDto, PermissionGroup>
    {
        public string Name { get; set; }

        public ICollection<GroupPermissionDto> GroupPermissions { get; set; }

    }
}