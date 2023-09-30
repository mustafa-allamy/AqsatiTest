using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Commands
{
    public class DeleteDepartmentVacationTypeCommand : ICommandHandler<DeleteDepartmentVacationTypeForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteDepartmentVacationTypeCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse> Handle(DeleteDepartmentVacationTypeForm command, CancellationToken cancellationToken)
        {
            await _dbContext.DepartmentVacationTypes
                .Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId)
                .ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse();
        }
    }
}