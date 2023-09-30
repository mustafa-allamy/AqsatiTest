using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Handlers.Commands
{
    public class CreateGeneralExcelTemplateCommand
    {
        public class Handler : ICommandHandler<CreateGeneralExcelTemplateForm, SuccessServiceResponse<GeneralExcelTemplateDto>>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async ValueTask<SuccessServiceResponse<GeneralExcelTemplateDto>> Handle(CreateGeneralExcelTemplateForm command, CancellationToken cancellationToken)
            {
                var generalExcelTemplate = command.ToEntity();

                await _dbContext.GeneralExcelTemplates.AddAsync(generalExcelTemplate, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var res = await _mediator.Send(new GetGeneralExcelTemplateForm() { Id = generalExcelTemplate.Id }, cancellationToken);

                return new SuccessServiceResponse<GeneralExcelTemplateDto>().WithData(res.AsT0.Data!);
            }
        }
    }
}