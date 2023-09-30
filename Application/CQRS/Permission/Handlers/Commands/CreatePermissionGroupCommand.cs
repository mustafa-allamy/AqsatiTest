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
    public class CreatePermissionGroupCommand :
        ICommandHandler<CreatePermissionGroupForm, OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreatePermissionGroupCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>> Handle(CreatePermissionGroupForm command, CancellationToken cancellationToken)
        {
            var permissionGroup = new PermissionGroup()
            {
                Name = command.Name,
                GroupPermissions = command.PermissionsIds.Select(x => new GroupPermission()
                {
                    PermissionId = x
                }).ToList()
            };

            await _dbContext.PermissionGroups.AddAsync(permissionGroup, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.PermissionGroups
                .Include(x => x.GroupPermissions).ThenInclude(x => x.Permission)
                .Where(x => x.Id == permissionGroup.Id)
                .Select(x => PermissionGroupDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<PermissionGroupDto>().WithData(res!);
        }
    }
}