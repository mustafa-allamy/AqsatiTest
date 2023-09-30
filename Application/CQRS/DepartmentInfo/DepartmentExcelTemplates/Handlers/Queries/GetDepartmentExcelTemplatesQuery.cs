using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Queries
{
    public class GetDepartmentExcelTemplatesQuery : IRequestHandler<GetDepartmentExcelTemplatesForm, SuccessServiceResponse<List<DepartmentExcelTemplateDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentExcelTemplatesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentExcelTemplateDto>>> Handle(GetDepartmentExcelTemplatesForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.DepartmentExcelTemplates
                .Where(x => x.DepartmentId == request.DepartmentId)
                .WhereIf(request.Name is not null, x => x.Name.Contains(request.Name))
                .Select(x => DepartmentExcelTemplateDto.FromEntity(x));


            return new SuccessServiceResponse<List<DepartmentExcelTemplateDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));
        }
    }
}