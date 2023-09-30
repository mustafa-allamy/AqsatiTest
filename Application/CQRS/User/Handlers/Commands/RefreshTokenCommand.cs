using Application.CQRS.User.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Infrastructure.Services;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using UserEntity = Domain.Entities.UserAndPermissions.User;
namespace Application.CQRS.User.Handlers.Commands
{
    public class RefreshTokenCommand : ICommandHandler<RefreshTokenForm, OneOf<SuccessServiceResponse<LoginResponseDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISharedCultureLocalizer _localizer;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommand(IApplicationDbContext dbContext, UserManager<UserEntity> userManager, ISharedCultureLocalizer localizer, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _localizer = localizer;
            _tokenService = tokenService;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<LoginResponseDto>, FailServiceResponse>> Handle(RefreshTokenForm command, CancellationToken cancellationToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(command.AccessToken);
            if (principal is null)
                return new FailServiceResponse().WithError("User.RefreshToken.InvalidToken");

            var userEmail = principal.Claims.First(x => x.Type == JwtRegisteredClaimNames.Name).Value;
            var user = await _userManager.Users
                .Include(x => x.UserPermissions).ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => x.Email == userEmail, cancellationToken);
            if (user == null || user.RefreshToken != command.RefreshToken || user.RefreshTokenExpire <= DateTime.UtcNow)
                return new FailServiceResponse().WithError(_localizer.GetLocalizedString("User.RefreshToken.InvalidToken"));

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpire = DateTime.UtcNow.AddDays(_tokenService.GetRefreshTokenExpireDate()).ToUniversalTime();
            await _userManager.UpdateAsync(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

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