using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Queries
{
    public class GetDepartmentVacationTypeQuery : IRequestHandler<GetDepartmentVacationTypeForm, OneOf<SuccessServiceResponse<DepartmentVacationTypeDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentVacationTypeQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<DepartmentVacationTypeDto>, FailServiceResponse>> Handle(GetDepartmentVacationTypeForm request, CancellationToken cancellationToken)
        {
            var departmentVacationType = await _dbContext.DepartmentVacationTypes
                .Where(x => x.Id == request.Id && x.DepartmentId == request.DepartmentId)
                .Include(x => x.VacationServiceRules).ThenInclude(x => x.Service)
                .Select(x => DepartmentVacationTypeDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return departmentVacationType is not null
                ? new SuccessServiceResponse<DepartmentVacationTypeDto>().WithData(departmentVacationType)
                : new FailServiceResponse();
        }
    }
}