using Application.CQRS.User.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.User.Forms
{
    public class GetUserForm : IRequest<OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
    {
        public int UserId { get; set; }
    }
}