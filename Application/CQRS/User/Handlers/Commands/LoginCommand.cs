using Application.CQRS.User.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Infrastructure.Services;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using UserEntity = Domain.Entities.UserAndPermissions.User;

namespace Application.CQRS.User.Handlers.Commands
{
    internal sealed class
        LoginCommand : ICommandHandler<LoginForm, OneOf<SuccessServiceResponse<LoginResponseDto>, FailServiceResponse>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public LoginCommand(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async ValueTask<OneOf<SuccessServiceResponse<LoginResponseDto>, FailServiceResponse>>
            Handle(LoginForm command, CancellationToken cancellationToken)
        {

            var user = await _userManager.Users.Include(x => x.UserPermissions).ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x =>
                        x.UserName!.Equals(command.Username),
                    cancellationToken: cancellationToken);

            if (user == null)
            {
                return new FailServiceResponse().WithError("User not found");
            }


            var authResult = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (!authResult.Succeeded)
            {
                return new FailServiceResponse().WithError("Login failed");
            }

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpire = DateTime.UtcNow.AddDays(_tokenService.GetRefreshTokenExpireDate()).ToUniversalTime();
            await _userManager.UpdateAsync(user);

            var response = new LoginResponseDto()
            {
                AuthToken = new JwtSecurityTokenHandler().WriteToken(token: _tokenService.GenerateToken(user)),
                Fullname = user.FullName,
                RefreshToken = user.RefreshToken,
                UserName = user.UserName,
                //Role = user.Role,
                Claims = user.UserPermissions.Select(y => y.Permission.PolicyName).ToList()
            };
            return new SuccessServiceResponse<LoginResponseDto>().WithData(response);
        }





    }
}
