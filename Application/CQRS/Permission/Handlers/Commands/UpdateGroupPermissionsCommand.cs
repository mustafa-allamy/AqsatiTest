using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Commands
{
    public class UpdateGroupPermissionsCommand : ICommandHandler<UpdateGroupPermissionsForm,
        OneOf<SuccessServiceResponse<List<GroupPermissionDto>>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGroupPermissionsCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<List<GroupPermissionDto>>, FailServiceResponse>> Handle(UpdateGroupPermissionsForm command, CancellationToken cancellationToken)
        {
            var groupPermissions = await _dbContext.GroupPermissions
                .Where(x => x.GroupId == command.Id)
                .Select(x => x.PermissionId)
                .ToListAsync(cancellationToken);

            var permissionsToAdd = command.PermissionsIds.Where(x => !groupPermissions.Contains(x)).ToList();
            var permissionsToRemove = groupPermissions.Where(x => !command.PermissionsIds.Contains(x)).ToList();

            await _dbContext.GroupPermissions.AddRangeAsync(permissionsToAdd.Select(x => new GroupPermission()
            {
                PermissionId = x,
                GroupId = command.Id,
            }), cancellationToken);
            await _dbContext.GroupPermissions.Where(x => permissionsToRemove.Contains(x.PermissionId))
                .ExecuteDeleteAsync(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.GroupPermissions.Where(x => x.GroupId == command.Id)
                .Include(x => x.Permission)
                .Select(x => GroupPermissionDto.FromEntity(x))
                .ToListAsync(cancellationToken);
            return new SuccessServiceResponse<List<GroupPermissionDto>>().WithData(res).WithCount(res.Count);
        }
    }
}