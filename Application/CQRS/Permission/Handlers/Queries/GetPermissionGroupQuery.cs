using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Queries
{
    public class GetPermissionGroupQuery : IRequestHandler<GetPermissionGroupForm, OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISharedCultureLocalizer _localizer;


        public GetPermissionGroupQuery(IApplicationDbContext dbContext, ISharedCultureLocalizer? localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<PermissionGroupDto>, FailServiceResponse>> Handle(GetPermissionGroupForm request, CancellationToken cancellationToken)
        {
            var permissionGroup = await _dbContext.PermissionGroups
                .Where(x => x.Id == request.Id)
                .Include(x => x.GroupPermissions).ThenInclude(x => x.Permission)
                .Select(x => PermissionGroupDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return permissionGroup != default ? new SuccessServiceResponse<PermissionGroupDto>().WithData(permissionGroup) :
                new FailServiceResponse().WithError(_localizer.GetLocalizedString("PermissionGroup.NotFound"));
        }
    }
}