using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Handlers.Commands
{
    public class CreateDepartmentServiceCommand : ICommandHandler<CreateDepartmentServiceForm, SuccessServiceResponse<DepartmentServiceDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateDepartmentServiceCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<DepartmentServiceDto>> Handle(CreateDepartmentServiceForm command, CancellationToken cancellationToken)
        {
            var departmentService = command.ToEntity();
            await _dbContext.DepartmentServices.AddAsync(departmentService, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<DepartmentServiceDto>().WithData(
                DepartmentServiceDto.FromEntity(departmentService));
        }
    }
}