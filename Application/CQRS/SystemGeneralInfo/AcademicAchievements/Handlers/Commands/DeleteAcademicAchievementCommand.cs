using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Handlers.Commands
{
    public class DeleteAcademicAchievementCommand : ICommandHandler<DeleteAcademicAchievementFrom, OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteAcademicAchievementCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>> Handle(DeleteAcademicAchievementFrom command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.AcademicAchievements.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            _dbContext.AcademicAchievements.Remove(entity!);
            await _dbContext.SaveChangesAsync();
            return new SuccessServiceResponse<bool>().WithData(true);

        }
    }
}
