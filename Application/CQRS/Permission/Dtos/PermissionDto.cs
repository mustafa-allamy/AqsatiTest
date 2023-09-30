using Common.Dto;

namespace Application.CQRS.Permission.Dtos
{
    public class PermissionDto : BaseDto<PermissionDto, Domain.Entities.UserAndPermissions.Permission>
    {
        public string PolicyName { get; set; }
        public string UserDefinedName { get; set; }
    }
}