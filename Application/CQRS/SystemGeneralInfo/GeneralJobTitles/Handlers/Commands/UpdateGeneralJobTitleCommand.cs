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
    public class UpdateGeneralJobTitleCommand : ICommandHandler<UpdateGeneralJobTitleForm, OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateGeneralJobTitleCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>> Handle(UpdateGeneralJobTitleForm command, CancellationToken cancellationToken)
        {

            var res = await _dbContext.GeneralJobTitles
                .Where(x => x.Id == command.Id)
                .ExecuteUpdateAsync(x => x.SetProperty(g => g.Name, y => command.Name), cancellationToken);


            var result = await _dbContext.GeneralJobTitles
                .Where(g => g.Id == command.Id)
                .Select(g => GeneralJobTitleDto.FromEntity(g))
                .FirstOrDefaultAsync(cancellationToken);

            return new SuccessServiceResponse<GeneralJobTitleDto>().WithData(result!);
        }
    }
}
