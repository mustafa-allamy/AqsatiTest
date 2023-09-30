using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.User.Handlers.Queries
{
    public class GetUserUnitsQuery : IRequestHandler<GetUserUnitsForm, SuccessServiceResponse<List<UnitDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUserUnitsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<UnitDto>>> Handle(GetUserUnitsForm request, CancellationToken cancellationToken)
        {
            var userUnits = await _dbContext.UserUnits.Where(x => x.UserId == request.UserId)
                .Include(x => x.Unit)
                .ToListAsync(cancellationToken: cancellationToken);

            var mainUnits = userUnits.Where(x => x.Unit.ParentId is null).Select(x => x.Id).ToList();
            mainUnits.AddRange(userUnits.Where(x => x.Unit.ParentId is not null && !mainUnits.Contains(x.Unit.ParentId.Value)).Select(x => x.Unit.ParentId!.Value).ToList());

            var units = await _dbContext.Units.Where(x => mainUnits.Contains(x.Id))
                .Include(x => x.SubUnits.Where(y => userUnits.Select(u => u.UnitId).Contains(y.Id)))
                .Select(x => UnitDto.FromEntity(x))
                .ToListAsync(cancellationToken);

            return new SuccessServiceResponse<List<UnitDto>>().WithData(units).WithCount(units.Count);

        }
    }
}