using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Handlers.Commands
{
    public class DeleteGeneralServiceCommand : ICommandHandler<DeleteGeneralServiceForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGeneralServiceCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse> Handle(DeleteGeneralServiceForm command, CancellationToken cancellationToken)
        {
            await _dbContext.GeneralServices.Where(x => x.Id == command.Id).ExecuteDeleteAsync(cancellationToken);
            return new SuccessServiceResponse();
        }
    }
}