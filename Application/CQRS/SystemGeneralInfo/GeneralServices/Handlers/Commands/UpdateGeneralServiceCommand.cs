using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Handlers.Commands
{
    public class UpdateGeneralServiceCommand : ICommandHandler<UpdateGeneralServiceForm, SuccessServiceResponse<GeneralServiceDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGeneralServiceCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<GeneralServiceDto>> Handle(UpdateGeneralServiceForm command, CancellationToken cancellationToken)
        {
            var generalService = await _dbContext.GeneralServices.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            generalService = command.ToEntity(generalService);

            _dbContext.GeneralServices.Update(generalService);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = await _dbContext.GeneralServices
                .Where(g => g.Id == command.Id)
                .Select(g => GeneralServiceDto.FromEntity(g))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<GeneralServiceDto>().WithData(result!);
        }
    }
}