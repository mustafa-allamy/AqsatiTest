using Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Handlers.Commands
{
    public class UpdateGeneralPositionCommand : ICommandHandler<UpdateGeneralPositionForm, OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGeneralPositionCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>> Handle(UpdateGeneralPositionForm command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GeneralPositions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            command.ToEntity(entity);
            _dbContext.GeneralPositions.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.GeneralPositions
                .Where(x => x.Id == entity.Id)
                .Select(x => GeneralPositionDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<GeneralPositionDto>().WithData(res!);

        }
    }
}
