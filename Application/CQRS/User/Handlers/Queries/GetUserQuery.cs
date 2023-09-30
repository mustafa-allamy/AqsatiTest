using Application.CQRS.User.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.User.Handlers.Queries
{
    public class GetUserQuery : IRequestHandler<GetUserForm, OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISharedCultureLocalizer _localizer;


        public GetUserQuery(IApplicationDbContext dbContext, ISharedCultureLocalizer? localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
            Handle(GetUserForm request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserPermissions).ThenInclude(x => x.Permission)
                .Include(x => x.UserPermissionGroups).ThenInclude(x => x.PermissionGroup)
                .Include(x => x.UserUnits)
                .Where(x => x.Id == request.UserId)
                .Select(x => UserDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return user != default ? new SuccessServiceResponse<UserDto>().WithData(user) :
                new FailServiceResponse().WithError(_localizer.GetLocalizedString("User.NotFound"));
        }
    }
}