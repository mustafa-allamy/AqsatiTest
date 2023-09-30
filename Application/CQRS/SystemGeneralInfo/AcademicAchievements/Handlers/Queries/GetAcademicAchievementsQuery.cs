using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos;
using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Handlers.Queries
{
    public class GetAcademicAchievementsQuery : IRequestHandler<GetAcademicAchievementsForm, SuccessServiceResponse<List<AcademicAchievementDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAcademicAchievementsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<AcademicAchievementDto>>> Handle(GetAcademicAchievementsForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.AcademicAchievements
                .WhereIf(request.Name != null, x => x.Name.Contains(request.Name))
                .WhereIf(request.GeneralServiceId != null, x => x.GeneralServiceId == request.GeneralServiceId)

                .Select(x => AcademicAchievementDto.FromEntity(x));

            return new SuccessServiceResponse<List<AcademicAchievementDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}
