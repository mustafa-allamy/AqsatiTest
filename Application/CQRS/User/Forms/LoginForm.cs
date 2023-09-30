using Application.CQRS.User.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.User.Forms
{
    public record LoginForm(string Username, string Password) : ICommand<OneOf<SuccessServiceResponse<LoginResponseDto>, FailServiceResponse>>
    {
    }
}
