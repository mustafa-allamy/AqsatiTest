using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Commands
{
    public class DeleteGeneralExcelTemplateCommand : ICommandHandler<DeleteGeneralExcelTemplateForm, SuccessServiceResponse<bool>>
    {
        private IApplicationDbContext _dbContext;

        public DeleteGeneralExcelTemplateCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<bool>> Handle(DeleteGeneralExcelTemplateForm command, CancellationToken cancellationToken)
        {
            await _dbContext.GeneralExcelTemplates.Where(x => x.Id == command.Id).ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse<bool>().WithData(true);
        }
    }
}