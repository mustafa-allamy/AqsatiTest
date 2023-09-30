using Common.Responses;
using Mediator;

namespace Application.CQRS.Permission.Forms
{
    public class DeleteUserPermissionGroupForm : ICommand<SuccessServiceResponse<bool>>
    {
        public int UserPermissionGroupId { get; set; }
    }
}