using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Handlers.Commands
{
    public class CreateUnitCommand : ICommandHandler<CreateUnitForm, SuccessServiceResponse<UnitDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateUnitCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<UnitDto>> Handle(CreateUnitForm command, CancellationToken cancellationToken)
        {
            var unit = command.ToEntity();
            await _dbContext.Units.AddAsync(unit, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<UnitDto>().WithData(UnitDto.FromEntity(unit));
        }
    }
}