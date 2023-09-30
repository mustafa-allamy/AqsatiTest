using Application.CQRS.Permission.Dtos;
using Common.Dto;
using Domain.Enums;

namespace Application.CQRS.User.Dtos
{
    public class UserDto : BaseDto<UserDto, Domain.Entities.UserAndPermissions.User>
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserRole UserRole { get; set; }

        public ICollection<UserPermissionDto> UserPermissions { get; set; }
        public ICollection<UserPermissionGroupDto> UserPermissionGroups { get; set; }
        public List<UserUnitDto> UserUnits { get; set; }
        public override void AddCustomMappings()
        {
            SetDtoCustomMappings()
                .Map(dest => dest.FullName, src => src.FullName);
        }
    }



}