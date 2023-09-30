using Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Handlers.Commands
{
    public class UpdateGeneralBankCommand : ICommandHandler<UpdateGeneralBankForm, OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGeneralBankCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>> Handle(UpdateGeneralBankForm command, CancellationToken cancellationToken)
        {

            var entity = await _dbContext.GeneralBanks.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            command.ToEntity(entity);
            _dbContext.GeneralBanks.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.GeneralBanks
                .Where(x => x.Id == entity.Id)
                .Select(x => GeneralBankDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<GeneralBankDto>().WithData(res!);
        }
    }
}
