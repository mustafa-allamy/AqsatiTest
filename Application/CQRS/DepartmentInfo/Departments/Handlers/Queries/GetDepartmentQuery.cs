using Application.CQRS.DepartmentInfo.Departments.Dtos;
using Application.CQRS.DepartmentInfo.Departments.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.DepartmentInfo.Departments.Handlers.Queries
{
    public class GetDepartmentQuery : IRequestHandler<GetDepartmentForm, OneOf<SuccessServiceResponse<DepartmentDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<DepartmentDto>, FailServiceResponse>> Handle(GetDepartmentForm request, CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments.Where(x => x.Id == request.Id)
                .Include(x => x.DepartmentReportSetting)
                .Include(x => x.DepartmentSetting)
                .Select(x => DepartmentDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return department is not null
                ? new SuccessServiceResponse<DepartmentDto>().WithData(department)
                : new FailServiceResponse();
        }
    }
}