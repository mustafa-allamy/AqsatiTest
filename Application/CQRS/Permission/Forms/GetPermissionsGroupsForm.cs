using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.Permission.Forms
{
    public class GetPermissionsGroupsForm : BaseQuery, IRequest<SuccessServiceResponse<List<PermissionGroupDto>>>
    {
        public string? Name { get; set; }
    }
}