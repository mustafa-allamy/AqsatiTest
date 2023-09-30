using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Handlers.Commands
{
    public class CreateGeneralServiceCommand : ICommandHandler<CreateGeneralServiceForm, SuccessServiceResponse<GeneralServiceDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateGeneralServiceCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<GeneralServiceDto>> Handle(CreateGeneralServiceForm command, CancellationToken cancellationToken)
        {
            var generalService = command.ToEntity();
            await _dbContext.GeneralServices.AddAsync(generalService, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<GeneralServiceDto>().WithData(
                GeneralServiceDto.FromEntity(generalService));
        }
    }
}