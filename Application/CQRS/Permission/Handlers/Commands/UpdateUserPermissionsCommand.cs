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
    public class UpdateUserPermissionsCommand : ICommandHandler<UpdateUserPermissionsForm, OneOf<SuccessServiceResponse<List<UserPermissionDto>>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateUserPermissionsCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<List<UserPermissionDto>>, FailServiceResponse>> Handle(UpdateUserPermissionsForm command, CancellationToken cancellationToken)
        {
            var userPermissions = await _dbContext.UserPermissions
                .Where(x => x.UserId == command.Id)
                .Select(x => x.PermissionId)
                .ToListAsync(cancellationToken);

            var permissionsToAdd = command.PermissionsIds
                    .Where(x => !userPermissions.Contains(x))
                    .Select(x => new UserPermission()
                    {
                        UserId = command.Id,
                        PermissionId = x
                    }).ToList();

            var permissionsToRemove = userPermissions.Where(x => !command.PermissionsIds.Contains(x)).ToList();

            await _dbContext.UserPermissions.AddRangeAsync(permissionsToAdd, cancellationToken);
            await _dbContext.UserPermissions.Where(x => permissionsToRemove.Contains(x.PermissionId))
                .ExecuteDeleteAsync(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.UserPermissions
                .Include(x => x.Permission)
                .Where(x => permissionsToAdd.Select(y => y.PermissionId).Contains(x.PermissionId) && x.UserId == command.Id)
                .Select(x => UserPermissionDto.FromEntity(x))
                .ToListAsync(cancellationToken);

            return new SuccessServiceResponse<List<UserPermissionDto>>().WithData(res).WithCount(res.Count);
        }
    }
}