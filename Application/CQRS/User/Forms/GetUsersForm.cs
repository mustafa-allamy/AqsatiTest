using Application.CQRS.User.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Enums;
using Mediator;

namespace Application.CQRS.User.Forms
{
    public class GetUsersForm : BaseQuery, IRequest<SuccessServiceResponse<List<UserDto>>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
        public int? DepartmentId { get; set; }
    }
}