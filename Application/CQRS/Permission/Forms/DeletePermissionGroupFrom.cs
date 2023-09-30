using Common.Forms;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;

namespace Application.CQRS.Permission.Forms
{
    public class DeletePermissionGroupFrom : BaseForm<DeletePermissionGroupFrom, PermissionGroup>, ICommand<SuccessServiceResponse<bool>>
    {

    }
}