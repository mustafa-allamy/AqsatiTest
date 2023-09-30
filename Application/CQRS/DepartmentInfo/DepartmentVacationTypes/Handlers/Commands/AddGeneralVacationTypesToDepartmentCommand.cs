using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Commands
{
    public class AddGeneralVacationTypesToDepartmentCommand : ICommandHandler<AddGeneralVacationTypesToDepartmentForm, SuccessServiceResponse<List<DepartmentVacationTypeDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddGeneralVacationTypesToDepartmentCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<DepartmentVacationTypeDto>>> Handle(AddGeneralVacationTypesToDepartmentForm command, CancellationToken cancellationToken)
        {
            var vacationsAdded = await _dbContext.DepartmentVacationTypes
                .Where(x => x.GeneralVacationTypeId != null && command.GeneralVacationTypeIds.Contains(x.GeneralVacationTypeId.Value))
                .Select(x => x.GeneralVacationTypeId)
                .ToListAsync(cancellationToken: cancellationToken);

            var departmentServices = await _dbContext.DepartmentServices
                .Where(x => x.GeneralServiceId != null && x.ParentServiceId == null && x.DepartmentId == command.DepartmentId)
                .ToListAsync(cancellationToken: cancellationToken);

            var vacationsToAdd = command.GeneralVacationTypeIds.Where(x => !vacationsAdded.Contains(x)).ToList();

            var departmentVacations = (await _dbContext.GeneralVacationTypes
                .Include(x => x.VacationServiceRules)
                .Where(x => vacationsToAdd.Contains(x.Id))
                .ToListAsync(cancellationToken))
                .Select(x => new DepartmentVacationType()
                {
                    DepartmentId = command.DepartmentId,
                    Name = x.Name,
                    EffectedByAllDeductions = x.EffectedByAllDeductions,
                    EffectedByAllAllowances = x.EffectedByAllAllowances,
                    Amount = x.Amount,
                    GeneralVacationTypeId = x.Id,


                    VacationServiceRules = x.VacationServiceRules
                        .Where(y => departmentServices.Select(z => z.GeneralServiceId).Contains(y.ServiceId))
                        .Select(y => new DepartmentVacationServiceRule()
                        {
                            ServiceId = departmentServices.First(z => z.GeneralServiceId == y.ServiceId).Id,
                            Amount = y.Amount,
                            NotEffectedByBasicSalaryDeduction = y.NotEffectedByBasicSalaryDeduction,
                        }).ToList()
                }).ToList();

            await _dbContext.DepartmentVacationTypes.AddRangeAsync(departmentVacations, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessServiceResponse<List<DepartmentVacationTypeDto>>()
                .WithData(departmentVacations.Select(x => DepartmentVacationTypeDto.FromEntity(x)).ToList())
                .WithCount(departmentVacations.Count);
        }
    }
}