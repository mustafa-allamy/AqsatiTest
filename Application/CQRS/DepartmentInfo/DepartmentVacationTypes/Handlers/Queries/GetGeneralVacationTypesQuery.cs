using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Queries
{
    public class GetDepartmentVacationTypesQuery : IRequestHandler<GetDepartmentVacationTypesForm,
        SuccessServiceResponse<List<DepartmentVacationTypeDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentVacationTypesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<List<DepartmentVacationTypeDto>>> Handle(
            GetDepartmentVacationTypesForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.DepartmentVacationTypes
                .Where(x => x.DepartmentId == request.DepartmentId)
                .WhereIf(request.Name is not null, x => x.Name.Contains(request.Name))
                .Select(x => DepartmentVacationTypeDto.FromEntity(x));

            return new SuccessServiceResponse<List<DepartmentVacationTypeDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}