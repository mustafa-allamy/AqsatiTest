using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Commands
{
    public class DeleteDepartmentExcelTemplateCommand : ICommandHandler<DeleteDepartmentExcelTemplateForm, SuccessServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteDepartmentExcelTemplateCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse> Handle(DeleteDepartmentExcelTemplateForm command, CancellationToken cancellationToken)
        {
            await _dbContext.DepartmentExcelTemplates
                .Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId)
                .ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse();
        }
    }
}