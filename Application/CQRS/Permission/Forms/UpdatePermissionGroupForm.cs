using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using OneOf;

namespace Application.CQRS.Permission.Forms
{
    public class UpdatePermissionGroupForm : BaseForm<UpdatePermissionGroupForm, PermissionGroup>,
        ICommand<OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>>
    {
        public string Name { get; set; }
    }
}