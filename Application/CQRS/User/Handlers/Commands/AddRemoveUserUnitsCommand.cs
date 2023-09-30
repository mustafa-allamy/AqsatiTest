using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.UserAndPermissions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.User.Handlers.Commands
{
    public class AddRemoveUserUnitsCommand : ICommandHandler<AddRemoveUserUnitsForm, SuccessServiceResponse<List<UnitDto>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public AddRemoveUserUnitsCommand(IApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async ValueTask<SuccessServiceResponse<List<UnitDto>>> Handle(AddRemoveUserUnitsForm command, CancellationToken cancellationToken)
        {
            var userUnits =
                await _dbContext.UserUnits.Where(x => x.UserId == command.UserId && x.UserId == command.UserId).Select(x => x.UnitId).ToListAsync(cancellationToken);

            var mainUnits = await _dbContext.Units
                .Where(x => command.UnitsIds.Contains(x.Id) && x.ParentId == null)
                .Include(x => x.SubUnits)
                .ToListAsync(cancellationToken);

            foreach (var mainUnit in mainUnits)
            {
                command.UnitsIds.AddRange(mainUnit.SubUnits.Select(x => x.Id));
            }

            command.UnitsIds = command.UnitsIds.Distinct().ToList();
            var addedUnits = new List<int>();
            foreach (var unitsId in command.UnitsIds)
            {
                if (!userUnits.Contains(unitsId))
                {
                    await _dbContext.UserUnits.AddAsync(new UserUnit()
                    {
                        UnitId = unitsId,
                        UserId = command.UserId
                    }
                    , cancellationToken);
                    addedUnits.Add(unitsId);
                }
            }

            var unitsToRemove = userUnits.Where(x => !command.UnitsIds.Contains(x)).ToList();

            if (unitsToRemove.Any())
                await _dbContext.UserUnits
                    .Where(x => x.UserId == command.UserId && unitsToRemove.Contains(x.UnitId))
                    .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _mediator.Send(new GetUserUnitsForm() { UserId = command.UserId });

            return new SuccessServiceResponse<List<UnitDto>>().WithData(res.Data).WithCount(res.Data?.Count ?? 0);
        }
    }
}