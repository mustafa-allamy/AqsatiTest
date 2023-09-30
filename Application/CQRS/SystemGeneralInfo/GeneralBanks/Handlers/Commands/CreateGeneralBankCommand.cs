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
    public class CreateGeneralBankCommand :
        ICommandHandler<CreateGeneralBankForm, OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateGeneralBankCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>> Handle(CreateGeneralBankForm command, CancellationToken cancellationToken)
        {
            var entity = command.ToEntity();

            await _dbContext.GeneralBanks.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.GeneralBanks
                .Where(x => x.Id == entity.Id)
                .Select(x => GeneralBankDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<GeneralBankDto>().WithData(res!);
        }
    }
}
