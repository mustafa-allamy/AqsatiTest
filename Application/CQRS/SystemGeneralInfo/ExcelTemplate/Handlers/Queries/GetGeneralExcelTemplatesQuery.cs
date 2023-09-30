using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Queries
{
    public class GetGeneralExcelTemplatesQuery : IRequestHandler<GetGeneralExcelTemplatesForm, SuccessServiceResponse<List<GeneralExcelTemplateDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralExcelTemplatesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<GeneralExcelTemplateDto>>> Handle(GetGeneralExcelTemplatesForm request, CancellationToken cancellationToken)
        {
            var query =
                 _dbContext.GeneralExcelTemplates.WhereIf(request.Name is not null,
                    x => x.Name.Contains(request.Name))
                     .Select(x => GeneralExcelTemplateDto.FromEntity(x));

            return new SuccessServiceResponse<List<GeneralExcelTemplateDto>>()
                .WithData(await query.ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));
        }
    }
}