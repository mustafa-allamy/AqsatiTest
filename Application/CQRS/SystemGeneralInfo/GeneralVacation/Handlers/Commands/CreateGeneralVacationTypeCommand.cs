using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Commands
{
    public class CreateGeneralVacationTypeCommand : ICommandHandler<CreateGeneralVacationTypeForm, SuccessServiceResponse<GeneralVacationTypeDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateGeneralVacationTypeCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<GeneralVacationTypeDto>> Handle(CreateGeneralVacationTypeForm command, CancellationToken cancellationToken)
        {
            var vacationType = command.ToEntity();
            await _dbContext.GeneralVacationTypes.AddAsync(vacationType, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var res = await _dbContext.GeneralVacationTypes
                .Where(x => x.Id == vacationType.Id)
                .Include(x => x.VacationServiceRules).ThenInclude(x => x.Service)
                .Select(x => GeneralVacationTypeDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<GeneralVacationTypeDto>().WithData(res!);
        }
    }
}