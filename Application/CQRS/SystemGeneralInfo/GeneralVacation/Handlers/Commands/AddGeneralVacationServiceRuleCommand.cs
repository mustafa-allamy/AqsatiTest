using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Commands
{
    public class AddGeneralVacationServiceRuleCommand : ICommandHandler<AddGeneralVacationServiceRuleForm, SuccessServiceResponse<GeneralVacationServiceRuleDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddGeneralVacationServiceRuleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<GeneralVacationServiceRuleDto>> Handle(AddGeneralVacationServiceRuleForm command, CancellationToken cancellationToken)
        {
            var vacationServiceRole = command.ToEntity();
            await _dbContext.GeneralVacationServiceRules.AddAsync(vacationServiceRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


            var res = await _dbContext.GeneralVacationServiceRules.Where(x => x.Id == vacationServiceRole.Id)
                .Include(x => x.Service)
                .Select(x => GeneralVacationServiceRuleDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<GeneralVacationServiceRuleDto>().WithData(res!);
        }
    }
}