using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Handlers.Commands
{
    public class CreateDepartmentVacationTypeCommand : ICommandHandler<CreateDepartmentVacationTypeForm, SuccessServiceResponse<DepartmentVacationTypeDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateDepartmentVacationTypeCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<DepartmentVacationTypeDto>> Handle(CreateDepartmentVacationTypeForm command, CancellationToken cancellationToken)
        {
            var vacationType = command.ToEntity();
            await _dbContext.DepartmentVacationTypes.AddAsync(vacationType, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var res = await _dbContext.DepartmentVacationTypes
                .Where(x => x.Id == vacationType.Id)
                .Include(x => x.VacationServiceRules).ThenInclude(x => x.Service)
                .Select(x => DepartmentVacationTypeDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<DepartmentVacationTypeDto>().WithData(res!);
        }
    }
}