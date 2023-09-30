using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using OneOf;

namespace Application.CQRS.Permission.Forms
{
    public class CreatePermissionGroupForm : BaseForm<CreatePermissionGroupForm, PermissionGroup>,
        ICommand<OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>>

    {
        public string Name { get; set; }
        public List<int> PermissionsIds { get; set; }

    }
}