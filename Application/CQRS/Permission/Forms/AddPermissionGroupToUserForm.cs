using Application.CQRS.Permission.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.Permission.Forms
{
    public class AddPermissionGroupToUserForm : ICommand<SuccessServiceResponse<UserPermissionGroupDto>>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int PermissionGroupId { get; set; }
    }
}