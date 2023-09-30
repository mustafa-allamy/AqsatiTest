using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Handlers.Commands
{
    public class CreateGeneralJobTitleCommand :
        ICommandHandler<CreateGeneralJobTitleForm, OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateGeneralJobTitleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>> Handle(CreateGeneralJobTitleForm command, CancellationToken cancellationToken)
        {
            var entity = command.ToEntity();

            await _dbContext.GeneralJobTitles.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _dbContext.GeneralJobTitles
                .Where(x => x.Id == entity.Id)
                .Select(x => GeneralJobTitleDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<GeneralJobTitleDto>().WithData(res!);
        }
    }
}
