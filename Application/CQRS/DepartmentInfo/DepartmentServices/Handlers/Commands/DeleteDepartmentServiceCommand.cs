using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Handlers.Commands
{
    public class DeleteDepartmentServiceCommand : ICommandHandler<DeleteDepartmentServiceForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteDepartmentServiceCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse> Handle(DeleteDepartmentServiceForm command, CancellationToken cancellationToken)
        {
            await _dbContext.DepartmentServices.Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId).ExecuteDeleteAsync(cancellationToken);
            return new SuccessServiceResponse();
        }
    }
}