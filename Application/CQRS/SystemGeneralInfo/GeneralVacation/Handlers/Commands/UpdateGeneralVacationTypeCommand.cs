using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Commands
{
    public class UpdateGeneralVacationTypeCommand : ICommandHandler<UpdateGeneralVacationTypeForm, SuccessServiceResponse<GeneralVacationTypeDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGeneralVacationTypeCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<GeneralVacationTypeDto>> Handle(UpdateGeneralVacationTypeForm command, CancellationToken cancellationToken)
        {
            var vacationType = await _dbContext.GeneralVacationTypes.Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);

            vacationType = command.ToEntity(vacationType);
            _dbContext.GeneralVacationTypes.Update(vacationType);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<GeneralVacationTypeDto>().WithData(
                GeneralVacationTypeDto.FromEntity(vacationType));
        }
    }
}