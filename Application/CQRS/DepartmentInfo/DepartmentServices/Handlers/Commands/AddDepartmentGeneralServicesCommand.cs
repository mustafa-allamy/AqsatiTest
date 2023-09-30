using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.Departments;
using Domain.Enums;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Handlers.Commands
{
    public class AddDepartmentGeneralServicesCommand : ICommandHandler<AddDepartmentServicesForm, SuccessServiceResponse<List<DepartmentServiceDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddDepartmentGeneralServicesCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentServiceDto>>> Handle(AddDepartmentServicesForm command, CancellationToken cancellationToken)
        {
            var servicesAdded = await _dbContext.DepartmentServices
                .Where(x => x.GeneralServiceId != null && command.GeneralServicesIds.Contains(x.GeneralServiceId.Value))
                .Select(x => x.GeneralServiceId)
                .ToListAsync(cancellationToken: cancellationToken);

            var servicesToAdd = command.GeneralServicesIds.Where(x => !servicesAdded.Contains(x)).ToList();

            var departmentServices = await _dbContext.GeneralServices
                .Include(x => x.ChildServices)
                .Where(x => servicesToAdd.Contains(x.Id))
                .Select(x => new DepartmentService()
                {
                    Amount = x.Amount,
                    DepartmentId = command.DepartmentId,
                    GeneralServiceId = x.Id,
                    IsPercentage = x.IsPercentage,
                    Kind = (ServiceKind)x.Kind!,
                    Priority = x.Priority,
                    Type = x.Type,
                    Name = x.Name,
                    ChildServices = x.ChildServices.Select(y => new DepartmentService()
                    {
                        Amount = y.Amount,
                        DepartmentId = command.DepartmentId,
                        GeneralServiceId = y.Id,
                        IsPercentage = y.IsPercentage,
                        Kind = (ServiceKind)y.Kind!,
                        Priority = y.Priority,
                        Type = y.Type,
                        Name = y.Name,
                    }).ToList()
                }).ToListAsync(cancellationToken);

            await _dbContext.DepartmentServices.AddRangeAsync(departmentServices, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<List<DepartmentServiceDto>>()
                .WithData(departmentServices.Select(x => DepartmentServiceDto.FromEntity(x)).ToList())
                .WithCount(departmentServices.Count);
        }
    }
}