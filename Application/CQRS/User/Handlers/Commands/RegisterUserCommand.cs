using Application.CQRS.User.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.AspNetCore.Identity;
using OneOf;
using UserEntity = Domain.Entities.UserAndPermissions.User;

namespace Application.CQRS.User.Handlers.Commands
{
    public class RegisterUserCommand
    {

        public class Handler : ICommandHandler<RegisterForm, OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
        {
            private readonly UserManager<UserEntity> _userManager;

            public Handler(UserManager<UserEntity> userManager)
            {
                _userManager = userManager;
            }
            public async ValueTask<OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>> Handle(RegisterForm request,
                CancellationToken cancellationToken)
            {
                var user = request.ToEntity();
                user.UserName = request.Email;
                var res = await _userManager.CreateAsync(user, request.Password);
                if (!res.Succeeded)
                    return new FailServiceResponse().WithErrors(res.Errors
                        .Select(x => new ResponseError(x.Description, x.Code)).ToList());
                return new SuccessServiceResponse<UserDto>().WithData(UserDto.FromEntity(user));
            }
        }
    }
}