using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Commands
{
    public class AddGeneralExcelTemplateServicesCommand : ICommandHandler<AddGeneralExcelTemplateServicesForm, SuccessServiceResponse<List<GeneralExcelTemplateServiceDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddGeneralExcelTemplateServicesCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<GeneralExcelTemplateServiceDto>>> Handle(AddGeneralExcelTemplateServicesForm command, CancellationToken cancellationToken)
        {
            var templateServices = await _dbContext.GeneralExcelTemplateServices
                .Where(x => x.GeneralExcelTemplateId == command.GeneralExcelTemplateId)
                .Select(x => x.ServiceId).ToListAsync(cancellationToken);

            var biggestOrder = await _dbContext.GeneralExcelTemplateServices
                .Where(x => x.GeneralExcelTemplateId == command.GeneralExcelTemplateId)
                .OrderBy(x => x.OrderNumber)
                .Select(x => x.OrderNumber)
                .LastOrDefaultAsync(cancellationToken);

            var servicesToAdd = command.Services.Where(x => !templateServices.Contains(x.ServiceId))
                .Select(x => x.ToEntity()).ToList();

            foreach (var service in servicesToAdd)
            {
                service.GeneralExcelTemplateId = command.GeneralExcelTemplateId;
                service.OrderNumber += biggestOrder;
            }

            await _dbContext.GeneralExcelTemplateServices.AddRangeAsync(servicesToAdd, cancellationToken);

            var res = await _dbContext.GeneralExcelTemplateServices
                .Where(x => servicesToAdd.Select(y => y.Id).Contains(x.Id))
                .Include(x => x.Service)
                .OrderBy(x => x.OrderNumber)
                .Select(x => GeneralExcelTemplateServiceDto.FromEntity(x))
                .ToListAsync(cancellationToken);

            return new SuccessServiceResponse<List<GeneralExcelTemplateServiceDto>>().WithData(res)
                .WithCount(res.Count);

        }
    }
}