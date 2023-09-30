using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Commands
{
    public class UpdateDepartmentExcelTemplateCommand : ICommandHandler<UpdateDepartmentExcelTemplateForm, SuccessServiceResponse<DepartmentExcelTemplateDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateDepartmentExcelTemplateCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<DepartmentExcelTemplateDto>> Handle(UpdateDepartmentExcelTemplateForm command, CancellationToken cancellationToken)
        {
            var template = await _dbContext.DepartmentExcelTemplates.Where(x => x.Id == command.Id)
                .Include(x => x.Services.OrderBy(o => o.OrderNumber)).ThenInclude(x => x.Service)
                .Include(x => x.Columns.OrderBy(o => o.Order)).ThenInclude(x => x.DefaultExcelTemplateColumn)
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

            _dbContext.DepartmentExcelTemplates.Update(template);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<DepartmentExcelTemplateDto>().WithData(
                DepartmentExcelTemplateDto.FromEntity(template));
        }
    }
}