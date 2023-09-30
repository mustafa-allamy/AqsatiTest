using Application.CQRS.Permission.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.Permission.Forms
{
    public class GetPermissionsForm : BaseQuery, IRequest<SuccessServiceResponse<List<PermissionDto>>>
    {
        public string? UserDefinedName { get; set; }
    }
}