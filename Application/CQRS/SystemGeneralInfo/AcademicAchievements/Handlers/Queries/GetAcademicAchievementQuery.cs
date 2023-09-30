using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos;
using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using Common.Extensions;
using Common.Responses;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Handlers.Queries
{
    public class GetAcademicAchievementQuery : IRequestHandler<GetAcademicAchievementForm, OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISharedCultureLocalizer _localizer;

        public GetAcademicAchievementQuery(IApplicationDbContext dbContext, ISharedCultureLocalizer? localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>> Handle(GetAcademicAchievementForm request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.AcademicAchievements
                .Where(x => x.Id == request.Id)
                .Include(x => x.GeneralService)
                .Select(x => AcademicAchievementDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return entity != default
                ? new SuccessServiceResponse<AcademicAchievementDto>().WithData(entity)
                : new FailServiceResponse().WithError(_localizer.GetLocalizedString("AcademicAchievement.NotFound"));
        }
    }
}
