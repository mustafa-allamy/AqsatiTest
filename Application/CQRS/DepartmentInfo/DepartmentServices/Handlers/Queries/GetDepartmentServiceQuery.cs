using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Handlers.Queries
{
    public class GetDepartmentServiceQuery : IRequestHandler<GetDepartmentServiceForm, OneOf<SuccessServiceResponse<DepartmentServiceDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentServiceQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<DepartmentServiceDto>, FailServiceResponse>> Handle(GetDepartmentServiceForm request, CancellationToken cancellationToken)
        {
            var departmentService = await _dbContext.DepartmentServices
                .Where(x => x.Id == request.Id && x.ParentServiceId == null && x.DepartmentId == request.DepartmentId)
                .Include(x => x.ChildServices)
                .Select(x => DepartmentServiceDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return departmentService is not null
                ? new SuccessServiceResponse<DepartmentServiceDto>().WithData(departmentService)
                : new FailServiceResponse();
        }
    }
}