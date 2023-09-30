using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Commands
{
    public class DeleteUserPermissionGroupCommand : ICommandHandler<DeleteUserPermissionGroupForm, SuccessServiceResponse<bool>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteUserPermissionGroupCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<bool>> Handle(DeleteUserPermissionGroupForm command, CancellationToken cancellationToken)
        {
            var GroupPermissions = await _dbContext.UserPermissionGroups
                .Include(x => x.PermissionGroup).ThenInclude(x => x.GroupPermissions)
                .Where(x => x.Id == command.UserPermissionGroupId)
                .SelectMany(x => x.PermissionGroup.GroupPermissions)
                .ToListAsync(cancellationToken);

            await _dbContext.UserPermissionGroups
                .Where(x => x.Id == command.UserPermissionGroupId)
                .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.UserPermissions
                .Where(x => GroupPermissions.Select(y => y.PermissionId).Contains(x.PermissionId))
                .ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse<bool>().WithData(true);
        }
    }
}