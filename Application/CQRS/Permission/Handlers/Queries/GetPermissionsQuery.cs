using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Queries
{
    public class GetPermissionsQuery : IRequestHandler<GetPermissionsForm, SuccessServiceResponse<List<PermissionDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetPermissionsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<PermissionDto>>> Handle(GetPermissionsForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Permissions
                .WhereIf(!string.IsNullOrEmpty(request.UserDefinedName),
                    x => x.UserDefinedName.Contains(request.UserDefinedName!))
                .Select(x => PermissionDto.FromEntity(x));

            return new SuccessServiceResponse<List<PermissionDto>>()
                .WithData(await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken: cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken: cancellationToken));
        }
    }
}