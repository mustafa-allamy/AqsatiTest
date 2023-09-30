using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.Permission.Forms
{
    public class UpdatePermissionForm : BaseForm<UpdatePermissionForm, Domain.Entities.UserAndPermissions.Permission>,
        ICommand<OneOf<SuccessServiceResponse<PermissionDto>, FailServiceResponse>>
    {

        public string? UserDefinedName { get; set; }

    }
}