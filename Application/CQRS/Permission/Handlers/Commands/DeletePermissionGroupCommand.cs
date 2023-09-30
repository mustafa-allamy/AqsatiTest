using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Commands
{
    public class DeletePermissionGroupCommand : ICommandHandler<DeletePermissionGroupFrom, SuccessServiceResponse<bool>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeletePermissionGroupCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<bool>> Handle(DeletePermissionGroupFrom command, CancellationToken cancellationToken)
        {
            await _dbContext.PermissionGroups.Where(x => x.Id == command.Id).ExecuteDeleteAsync(cancellationToken);

            return new SuccessServiceResponse<bool>().WithData(true);
        }
    }
}