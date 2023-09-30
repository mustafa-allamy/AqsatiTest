using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Commands
{
    public class DeleteGeneralVacationServiceRuleCommand : ICommandHandler<DeleteGeneralVacationServiceRuleForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGeneralVacationServiceRuleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse> Handle(DeleteGeneralVacationServiceRuleForm command, CancellationToken cancellationToken)
        {
            await _dbContext.GeneralVacationServiceRules.Where(x => x.Id == command.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse();
        }
    }
}