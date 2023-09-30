using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Commands
{
    public class DeleteGeneralVacationTypeCommand : ICommandHandler<DeleteGeneralVacationTypeForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGeneralVacationTypeCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse> Handle(DeleteGeneralVacationTypeForm command, CancellationToken cancellationToken)
        {
            await _dbContext.GeneralVacationTypes.Where(x => x.Id == command.Id).ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse();
        }
    }
}