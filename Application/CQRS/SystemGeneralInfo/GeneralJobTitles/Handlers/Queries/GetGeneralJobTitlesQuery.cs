using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Handlers.Queries
{
    public class GetGeneralJobTitlesQuery : IRequestHandler<GetGeneralJobTitlesForm, SuccessServiceResponse<List<GeneralJobTitleDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralJobTitlesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<GeneralJobTitleDto>>> Handle(GetGeneralJobTitlesForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GeneralJobTitles
                .WhereIf(request.Name != null, x => x.Name.Contains(request.Name))
                .Select(x => GeneralJobTitleDto.FromEntity(x));

            return new SuccessServiceResponse<List<GeneralJobTitleDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}
