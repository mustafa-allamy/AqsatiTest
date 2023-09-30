using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Queries
{
    public class GetDefaultExcelTemplateQuery
    {
        public class Handler : IRequestHandler<GetDefaultExcelTemplateForm, SuccessServiceResponse<List<DefaultExcelTemplateColumnDto>>>
        {
            private readonly IApplicationDbContext _dbContext;

            public Handler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async ValueTask<SuccessServiceResponse<List<DefaultExcelTemplateColumnDto>>> Handle(GetDefaultExcelTemplateForm request, CancellationToken cancellationToken)
            {
                var defaultExcelTemplateColumns = await _dbContext.DefaultExcelTemplateColumns
                    .Select(x => DefaultExcelTemplateColumnDto.FromEntity(x))
                    .ToListAsync(cancellationToken);

                return new SuccessServiceResponse<List<DefaultExcelTemplateColumnDto>>().WithData(
                    defaultExcelTemplateColumns);
            }
        }
    }
}