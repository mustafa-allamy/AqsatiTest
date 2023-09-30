using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Commands
{
    public class
        DeleteDepartmentVacationServiceRuleCommand : ICommandHandler<DeleteDepartmentVacationServiceRuleForm,
            SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteDepartmentVacationServiceRuleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse> Handle(DeleteDepartmentVacationServiceRuleForm command,
            CancellationToken cancellationToken)
        {
            await _dbContext.DepartmentVacationServiceRules.Where(x => x.Id == command.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse();
        }
    }
}