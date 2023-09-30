using Application.CQRS.Permission.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.Permission.Forms
{
    public class GetPermissionGroupForm : IRequest<OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}