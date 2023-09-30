using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Handlers.Commands
{
    public class DeleteGeneralBankCommand : ICommandHandler<DeleteGeneralBankFrom, OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGeneralBankCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>> Handle(DeleteGeneralBankFrom command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GeneralBanks.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            _dbContext.GeneralBanks.Remove(entity!);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessServiceResponse<bool>().WithData(true);
        }
    }
}
