using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos;
using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Handlers.Commands
{
    public class UpdateAcademicAchievementCommand : ICommandHandler<UpdateAcademicAchievementForm, OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateAcademicAchievementCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>> Handle(UpdateAcademicAchievementForm command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.AcademicAchievements.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            command.ToEntity(entity);
            _dbContext.AcademicAchievements.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.AcademicAchievements
                .Where(x => x.Id == entity.Id)
                .Select(x => AcademicAchievementDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<AcademicAchievementDto>().WithData(res!);

        }
    }
}
