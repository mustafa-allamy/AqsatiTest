using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Handlers.Queries
{
    public class GetDepartmentServicesQuery : IRequestHandler<GetDepartmentServicesForm, SuccessServiceResponse<List<DepartmentServiceDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentServicesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentServiceDto>>> Handle(GetDepartmentServicesForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.DepartmentServices
                .Include(x => x.ChildServices)
                .Where(x => x.Type == request.Type && x.ParentServiceId == null && x.DepartmentId == request.DepartmentId)
                .WhereIf(request.Name is not null, x => x.Name.Contains(request.Name))
                .WhereIf(request.IsPercentage is not null, x => x.IsPercentage == request.IsPercentage)
                .WhereIf(request.Kind is not null, x => x.Kind == request.Kind)
                .Select(x => DepartmentServiceDto.FromEntity(x));

            return new SuccessServiceResponse<List<DepartmentServiceDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));
        }
    }
}