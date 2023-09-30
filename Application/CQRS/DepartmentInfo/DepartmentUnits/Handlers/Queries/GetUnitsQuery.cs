using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Handlers.Queries
{
    public class GetUnitsQuery : IRequestHandler<GetUnitsForm, SuccessServiceResponse<List<UnitDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUnitsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<UnitDto>>> Handle(GetUnitsForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Units.Where(x => x.DepartmentId == request.DepartmentId && x.ParentId == null)
                .Include(x => x.SubUnits)
                .Select(x => UnitDto.FromEntity(x));


            return new SuccessServiceResponse<List<UnitDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken)).WithCount(query.Count());
        }
    }
}