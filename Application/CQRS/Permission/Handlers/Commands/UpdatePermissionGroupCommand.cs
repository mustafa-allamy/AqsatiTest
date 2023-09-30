using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Commands
{
    public class UpdatePermissionGroupCommand : ICommandHandler<UpdatePermissionGroupForm, OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdatePermissionGroupCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>> Handle(UpdatePermissionGroupForm command, CancellationToken cancellationToken)
        {
            var updateRes =
                await _dbContext.PermissionGroups.Where(x => x.Id == command.Id)
                    .ExecuteUpdateAsync(x => x.SetProperty(group =>
                        group.Name, y => command.Name), cancellationToken: cancellationToken);
            var res = PermissionGroupDto.FromEntity(
                (await _dbContext.PermissionGroups.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken))!);
            return updateRes == 1 ? new SuccessServiceResponse<PermissionGroupDto>()
                    .WithData(res)
                : new FailServiceResponse();
        }
    }
}