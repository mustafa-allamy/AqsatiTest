using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Commands
{
    public class UpdateDepartmentVacationTypeCommand : ICommandHandler<UpdateDepartmentVacationTypeForm,
        SuccessServiceResponse<DepartmentVacationTypeDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateDepartmentVacationTypeCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<DepartmentVacationTypeDto>> Handle(
            UpdateDepartmentVacationTypeForm command, CancellationToken cancellationToken)
        {
            var vacationType = await _dbContext.DepartmentVacationTypes
                .Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId)
                .FirstOrDefaultAsync(cancellationToken);

            vacationType = command.ToEntity(vacationType);
            _dbContext.DepartmentVacationTypes.Update(vacationType);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<DepartmentVacationTypeDto>().WithData(
                DepartmentVacationTypeDto.FromEntity(vacationType));
        }
    }
}