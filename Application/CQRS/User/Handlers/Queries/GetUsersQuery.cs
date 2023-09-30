using Application.CQRS.User.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.User.Handlers.Queries
{
    public class GetUsersQuery : IRequestHandler<GetUsersForm, SuccessServiceResponse<List<UserDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUsersQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<UserDto>>> Handle(GetUsersForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users
                .Where(x => x.DepartmentId == request.DepartmentId)
                .WhereIf(request.Name is not null, x => x.FullName.Contains(request.Name))
                .WhereIf(request.Email is not null, x => x.Email.Contains(request.Email))
                //.WhereIf(request.Name is not null, x => x.Role.Equals(request.Role))
                .Select(x => UserDto.FromEntity(x));

            return new SuccessServiceResponse<List<UserDto>>()
                .WithData(await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));
        }
    }
}