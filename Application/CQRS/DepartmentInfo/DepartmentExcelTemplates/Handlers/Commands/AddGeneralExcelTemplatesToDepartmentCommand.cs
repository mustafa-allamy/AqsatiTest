using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Commands
{
    public class AddGeneralExcelTemplatesToDepartmentCommand : ICommandHandler<AddGeneralExcelTemplatesToDepartmentForm, SuccessServiceResponse<List<DepartmentExcelTemplateDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddGeneralExcelTemplatesToDepartmentCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentExcelTemplateDto>>> Handle(AddGeneralExcelTemplatesToDepartmentForm command, CancellationToken cancellationToken)
        {
            var templatesAdded = await _dbContext.DepartmentExcelTemplates
                .Where(x => x.GeneralExcelTemplateId != null && command.GeneralExcelTemplateIds.Contains(x.GeneralExcelTemplateId.Value))
                .Select(x => x.GeneralExcelTemplateId)
                .ToListAsync(cancellationToken: cancellationToken);

            var departmentServices = await _dbContext.DepartmentServices.Where(x => x.GeneralServiceId != null && x.ParentServiceId == null && x.DepartmentId == command.DepartmentId).ToListAsync(cancellationToken: cancellationToken);

            var templatesToAdd = command.GeneralExcelTemplateIds.Where(x => !templatesAdded.Contains(x)).ToList();

            var departmentTemplates = (await _dbContext.GeneralExcelTemplates
                .Include(x => x.Services)
                .Include(x => x.Columns)
                .Where(x => templatesToAdd.Contains(x.Id))
                .ToListAsync(cancellationToken))
                .Select(x => new DepartmentExcelTemplate()
                {
                    DepartmentId = command.DepartmentId,
                    Name = x.Name,
                    GeneralExcelTemplateId = x.Id,
                    Columns = x.Columns.Select(y => new DepartmentExcelTemplateColumns()
                    {
                        DefaultExcelTemplateColumnId = y.DefaultExcelTemplateColumnId,
                        DisplayName = y.DisplayName,
                        IsVisible = y.IsVisible,
                        Order = y.Order,
                    }).ToList(),

                    Services = x.Services.Where(y => departmentServices.Select(z => z.GeneralServiceId).Contains(y.ServiceId)).Select(y => new DepartmentExcelTemplateService()
                    {
                        IsVisible = y.IsVisible,
                        AlternativeName = y.AlternativeName,
                        OrderNumber = y.OrderNumber,
                        ShowChildService = y.ShowChildService,
                        ServiceId = departmentServices.First(z => z.GeneralServiceId == y.ServiceId).Id,

                    }).ToList()
                }).ToList();

            await _dbContext.DepartmentExcelTemplates.AddRangeAsync(departmentTemplates, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessServiceResponse<List<DepartmentExcelTemplateDto>>()
                .WithData(departmentTemplates.Select(x => DepartmentExcelTemplateDto.FromEntity(x)).ToList())
                .WithCount(departmentTemplates.Count);
        }
    }
}