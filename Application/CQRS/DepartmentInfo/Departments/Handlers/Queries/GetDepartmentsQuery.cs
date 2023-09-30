using Application.CQRS.DepartmentInfo.Departments.Dtos;
using Application.CQRS.DepartmentInfo.Departments.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.Departments.Handlers.Queries
{
    public class GetDepartmentsQuery : IRequestHandler<GetDepartmentsForm, SuccessServiceResponse<List<DepartmentDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentDto>>> Handle(GetDepartmentsForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Departments
                .WhereIf(request.MinistryId is not null, x => x.MinistryId == request.MinistryId)
                .WhereIf(request.Name is not null, x => x.Name.Contains(request.Name))
                .WhereIf(request.Domain is not null, x => x.Name.Contains(request.Domain))
                .Select(x => DepartmentDto.FromEntity(x));

            return new SuccessServiceResponse<List<DepartmentDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));
        }
    }
}