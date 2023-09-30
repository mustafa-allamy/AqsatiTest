using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Commands
{
    public class AddPermissionGroupToUserCommand : ICommandHandler<AddPermissionGroupToUserForm, SuccessServiceResponse<UserPermissionGroupDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddPermissionGroupToUserCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<UserPermissionGroupDto>> Handle(AddPermissionGroupToUserForm command, CancellationToken cancellationToken)
        {
            var userPermissions = await _dbContext.UserPermissions
                .Where(x => x.UserId == command.UserId)
                .Select(x => x.PermissionId)
                .ToListAsync(cancellationToken);

            var groupPermissions = await _dbContext.GroupPermissions
                .Where(x => x.GroupId == command.PermissionGroupId)
                .Select(x => x.PermissionId)
                .ToListAsync(cancellationToken);

            var permissionsToAdd = groupPermissions
                .Where(x => !userPermissions.Contains(x))
                .Select(x => new UserPermission()
                {
                    UserId = command.UserId,
                    PermissionId = x
                }).ToList();
            var userPermissionGroup = new UserPermissionGroup()
            {
                UserId = command.UserId,
                PermissionGroupId = command.PermissionGroupId
            };
            await _dbContext.UserPermissions.AddRangeAsync(permissionsToAdd, cancellationToken);
            await _dbContext.UserPermissionGroups.AddAsync(userPermissionGroup);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.UserPermissionGroups
                .Where(x => x.Id == userPermissionGroup.Id)
                .Include(x => x.PermissionGroup)
                .Select(x => UserPermissionGroupDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<UserPermissionGroupDto>().WithData(res!);
        }
    }
}