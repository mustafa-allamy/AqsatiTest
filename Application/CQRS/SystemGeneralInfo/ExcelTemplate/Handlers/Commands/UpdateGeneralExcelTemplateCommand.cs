using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Commands
{
    public class UpdateGeneralExcelTemplateCommand : ICommandHandler<UpdateGeneralExcelTemplateForm, SuccessServiceResponse<GeneralExcelTemplateDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGeneralExcelTemplateCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<GeneralExcelTemplateDto>> Handle(UpdateGeneralExcelTemplateForm command, CancellationToken cancellationToken)
        {
            var template = await _dbContext.GeneralExcelTemplates.Where(x => x.Id == command.Id)
                .Include(x => x.Services).ThenInclude(x => x.Service)
                .Include(x => x.Columns).ThenInclude(x => x.DefaultExcelTemplateColumn)
                .FirstOrDefaultAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(command.Name))
                template.Name = command.Name;

            if (command.Columns.Any())
                foreach (var templateColumn in template.Columns)
                {
                    var commandColumn = command.Columns.FirstOrDefault(x => x.Id == templateColumn.Id);
                    commandColumn!.ToEntity(templateColumn);
                }

            if (command.Services.Any())
                foreach (var templateService in template.Services)
                {
                    var commandService = command.Services.FirstOrDefault(x => x.Id == templateService.Id);
                    commandService!.ToEntity(templateService);
                }

            _dbContext.GeneralExcelTemplates.Update(template);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<GeneralExcelTemplateDto>().WithData(
                GeneralExcelTemplateDto.FromEntity(template));
        }
    }
}