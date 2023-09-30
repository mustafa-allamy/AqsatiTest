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
    public class CreateGeneralPositionCommand :
        ICommandHandler<CreateGeneralPositionForm, OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateGeneralPositionCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>> Handle(CreateGeneralPositionForm command, CancellationToken cancellationToken)
        {
            var entity = command.ToEntity();

            await _dbContext.GeneralPositions.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.GeneralPositions
                .Where(x => x.Id == entity.Id)
                .Select(x => GeneralPositionDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<GeneralPositionDto>().WithData(res!);
        }
    }
}
