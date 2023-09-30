using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Handlers.Commands
{
    public class CreateDepartmentExcelTemplateCommand : ICommandHandler<CreateDepartmentExcelTemplateForm, SuccessServiceResponse<DepartmentExcelTemplateDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateDepartmentExcelTemplateCommand(IApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async ValueTask<SuccessServiceResponse<DepartmentExcelTemplateDto>> Handle(CreateDepartmentExcelTemplateForm command, CancellationToken cancellationToken)
        {
            var departmentExcelTemplate = command.ToEntity();

            await _dbContext.DepartmentExcelTemplates.AddAsync(departmentExcelTemplate, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var res = await _mediator
                .Send(new GetDepartmentExcelTemplateForm() { Id = departmentExcelTemplate.Id, DepartmentId = departmentExcelTemplate.DepartmentId }, cancellationToken);
            return new SuccessServiceResponse<DepartmentExcelTemplateDto>().WithData(res.AsT0.Data!)!;
        }
    }
}