using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using OneOf;

namespace Application.CQRS.Permission.Forms
{
    public class UpdateUserPermissionsForm : BaseForm<UpdateUserPermissionsForm, UserPermission>,
        ICommand<OneOf<SuccessServiceResponse<List<UserPermissionDto>>, FailServiceResponse>>
    {
        public List<int> PermissionsIds { get; set; }
    }
}