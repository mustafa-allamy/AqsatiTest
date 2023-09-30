using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Queries
{
    public class GetDepartmentExcelTemplateQuery : IRequestHandler<GetDepartmentExcelTemplateForm, OneOf<SuccessServiceResponse<DepartmentExcelTemplateDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetDepartmentExcelTemplateQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<DepartmentExcelTemplateDto>, FailServiceResponse>> Handle(GetDepartmentExcelTemplateForm request, CancellationToken cancellationToken)
        {
            var template = await _dbContext.DepartmentExcelTemplates
                .WhereIf(request.DepartmentId is not null, x => x.DepartmentId == request.DepartmentId)
                .Where(x => x.Id == request.Id)
                .Include(x => x.Services.OrderBy(o => o.OrderNumber))
                .Include(x => x.Columns.OrderBy(o => o.Order)).ThenInclude(x => x.DefaultExcelTemplateColumn)
                .Select(x => DepartmentExcelTemplateDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return template is not null ? new SuccessServiceResponse<DepartmentExcelTemplateDto>().WithData(template) : new FailServiceResponse();
        }
    }
}