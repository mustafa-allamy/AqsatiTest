using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Handlers.Commands
{
    public class DeleteGeneralPositionCommand : ICommandHandler<DeleteGeneralPositionFrom, OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGeneralPositionCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>> Handle(DeleteGeneralPositionFrom command, CancellationToken cancellationToken)
        {
            await _dbContext.GeneralPositions.Where(x => x.Id == command.Id).ExecuteDeleteAsync(cancellationToken);
            return new SuccessServiceResponse<bool>().WithData(true);

        }
    }
}
