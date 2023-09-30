using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Queries
{
    public class GetPermissionsGroupsQuery : IRequestHandler<GetPermissionsGroupsForm, SuccessServiceResponse<List<PermissionGroupDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetPermissionsGroupsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<PermissionGroupDto>>> Handle(GetPermissionsGroupsForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.PermissionGroups
                .WhereIf(request.Name != null, x => x.Name.Contains(request.Name))
                .Select(x => PermissionGroupDto.FromEntity(x));

            return new SuccessServiceResponse<List<PermissionGroupDto>>()
                .WithData(await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}