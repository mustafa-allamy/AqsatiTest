using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Handlers.Commands
{
    public class UpdateUnitCommand : ICommandHandler<UpdateUnitForm, SuccessServiceResponse<UnitDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateUnitCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<UnitDto>> Handle(UpdateUnitForm command, CancellationToken cancellationToken)
        {
            await _dbContext.Units.Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId)
               .ExecuteUpdateAsync(x => x.SetProperty(y => y.Name, command.Name), cancellationToken: cancellationToken);

            var res = await _dbContext.Units.Where(x => x.Id == command.Id && x.DepartmentId == command.DepartmentId).Select(x => UnitDto.FromEntity(x)).FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<UnitDto>().WithData(res);
        }
    }
}