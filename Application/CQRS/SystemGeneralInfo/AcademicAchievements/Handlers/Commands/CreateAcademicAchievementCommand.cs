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
    public class CreateAcademicAchievementCommand :
        ICommandHandler<CreateAcademicAchievementForm, OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateAcademicAchievementCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>> Handle(CreateAcademicAchievementForm command, CancellationToken cancellationToken)
        {
            var entity = command.ToEntity();

            await _dbContext.AcademicAchievements.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.AcademicAchievements
                .Where(x => x.Id == entity.Id)
                .Select(x => AcademicAchievementDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<AcademicAchievementDto>().WithData(res!);
        }
    }
}
