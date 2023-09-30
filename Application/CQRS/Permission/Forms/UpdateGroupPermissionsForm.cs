using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using OneOf;

namespace Application.CQRS.Permission.Forms
{
    public class UpdateGroupPermissionsForm : BaseForm<UpdateGroupPermissionsForm, PermissionGroup>,
        ICommand<OneOf<SuccessServiceResponse<List<GroupPermissionDto>>, FailServiceResponse>>
    {
        public List<int> PermissionsIds { get; set; }
    }
}