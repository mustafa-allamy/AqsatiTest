using Application.CQRS.User.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.User.Forms
{
    public class RefreshTokenForm : ICommand<OneOf<SuccessServiceResponse<LoginResponseDto>, FailServiceResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}