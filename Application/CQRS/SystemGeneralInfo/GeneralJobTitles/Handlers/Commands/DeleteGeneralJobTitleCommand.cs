using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Handlers.Commands
{
    public class DeleteGeneralJobTitleCommand : ICommandHandler<DeleteGeneralJobTitleFrom, OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGeneralJobTitleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>> Handle(DeleteGeneralJobTitleFrom command, CancellationToken cancellationToken)
        {
            await _dbContext.GeneralJobTitles.Where(x => x.Id == command.Id).ExecuteDeleteAsync(cancellationToken);
            return new SuccessServiceResponse<bool>().WithData(true);
        }
    }
}
