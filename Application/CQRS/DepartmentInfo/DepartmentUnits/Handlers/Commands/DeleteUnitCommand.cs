using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Handlers.Commands
{
    public class DeleteUnitCommand : ICommandHandler<DeleteUnitForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteUnitCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse> Handle(DeleteUnitForm command, CancellationToken cancellationToken)
        {
            await _dbContext.Units.Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId)
                .ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse();
        }
    }
}