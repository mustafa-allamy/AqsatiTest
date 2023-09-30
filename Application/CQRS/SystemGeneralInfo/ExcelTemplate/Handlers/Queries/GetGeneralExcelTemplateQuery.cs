using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Queries
{
    public class GetGeneralExcelTemplateQuery : IRequestHandler<GetGeneralExcelTemplateForm, OneOf<SuccessServiceResponse<GeneralExcelTemplateDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralExcelTemplateQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralExcelTemplateDto>, FailServiceResponse>> Handle(GetGeneralExcelTemplateForm request, CancellationToken cancellationToken)
        {
            var res = await _dbContext.GeneralExcelTemplates.Where(x => x.Id == request.Id)
                .Include(x => x.Services.OrderBy(o => o.OrderNumber)).ThenInclude(x => x.Service)
                .Include(x => x.Columns.OrderBy(o => o.Order)).ThenInclude(x => x.DefaultExcelTemplateColumn)
                .Select(x => GeneralExcelTemplateDto.FromEntity(x)).FirstOrDefaultAsync(cancellationToken);

            return res is not null
                ? new SuccessServiceResponse<GeneralExcelTemplateDto>().WithData(res)
                : new FailServiceResponse().WithError("ItemNotFound");
        }
    }
}