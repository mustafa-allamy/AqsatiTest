using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Commands
{
    public class AddDepartmentExcelTemplateServicesCommand : ICommandHandler<AddDepartmentExcelTemplateServicesForm, SuccessServiceResponse<List<DepartmentExcelTemplateServiceDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddDepartmentExcelTemplateServicesCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentExcelTemplateServiceDto>>> Handle(AddDepartmentExcelTemplateServicesForm command, CancellationToken cancellationToken)
        {
            var templateServices = await _dbContext.DepartmentExcelTemplateServices
                .Where(x => x.DepartmentExcelTemplateId == command.DepartmentExcelTemplateId)
                .Select(x => x.ServiceId).ToListAsync(cancellationToken);

            var biggestOrder = await _dbContext.DepartmentExcelTemplateServices
                .Where(x => x.DepartmentExcelTemplateId == command.DepartmentExcelTemplateId)
                .OrderBy(x => x.OrderNumber)
                .Select(x => x.OrderNumber)
                .LastOrDefaultAsync(cancellationToken);

            var servicesToAdd = command.Services.Where(x => !templateServices.Contains(x.ServiceId))
                .Select(x => x.ToEntity()).ToList();

            foreach (var service in servicesToAdd)
            {
                service.DepartmentExcelTemplateId = command.DepartmentExcelTemplateId;
                service.OrderNumber += biggestOrder;
            }

            await _dbContext.DepartmentExcelTemplateServices.AddRangeAsync(servicesToAdd, cancellationToken);

            var res = await _dbContext.DepartmentExcelTemplateServices
                .Where(x => servicesToAdd.Select(y => y.Id).Contains(x.Id))
                .Include(x => x.Service)
                .OrderBy(x => x.OrderNumber)
                .Select(x => DepartmentExcelTemplateServiceDto.FromEntity(x))
                .ToListAsync(cancellationToken);

            return new SuccessServiceResponse<List<DepartmentExcelTemplateServiceDto>>().WithData(res)
                .WithCount(res.Count);
        }
    }
}