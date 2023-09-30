using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Commands
{
    public class AddDepartmentVacationServiceRuleCommand : ICommandHandler<AddDepartmentVacationServiceRuleForm, SuccessServiceResponse<DepartmentVacationServiceRuleDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddDepartmentVacationServiceRuleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<DepartmentVacationServiceRuleDto>> Handle(AddDepartmentVacationServiceRuleForm command, CancellationToken cancellationToken)
        {
            var vacationServiceRole = command.ToEntity();
            await _dbContext.DepartmentVacationServiceRules.AddAsync(vacationServiceRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


            var res = await _dbContext.DepartmentVacationServiceRules.Where(x => x.Id == vacationServiceRole.Id)
                .Include(x => x.Service)
                .Select(x => DepartmentVacationServiceRuleDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<DepartmentVacationServiceRuleDto>().WithData(res!);
        }
    }
}