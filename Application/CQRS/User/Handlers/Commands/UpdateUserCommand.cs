using Application.CQRS.User.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.User.Handlers.Commands
{
    public class UpdateUserCommand : ICommandHandler<UpdateUserForm, OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly UserManager<Domain.Entities.UserAndPermissions.User> _userManager;

        public UpdateUserCommand(IApplicationDbContext dbContext, UserManager<Domain.Entities.UserAndPermissions.User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>> Handle(UpdateUserForm command, CancellationToken cancellationToken)
        {
            var user = (await _userManager.Users.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken))!;

            command.ToEntity(user);
            if (command.Password is not null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, command.Password);
            }
            var result = await _userManager.UpdateAsync(user);


            return result.Succeeded ? new SuccessServiceResponse<UserDto>()
                .WithData(UserDto.FromEntity(user)) : new FailServiceResponse();
        }
    }
}