using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Handlers.Commands
{
    public class UpdateDepartmentServiceCommand : ICommandHandler<UpdateDepartmentServiceForm, SuccessServiceResponse<DepartmentServiceDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateDepartmentServiceCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<DepartmentServiceDto>> Handle(UpdateDepartmentServiceForm command, CancellationToken cancellationToken)
        {
            var departmentService = await _dbContext.DepartmentServices.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            departmentService = command.ToEntity(departmentService);

            _dbContext.DepartmentServices.Update(departmentService);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = await _dbContext.DepartmentServices
                .Where(g => g.Id == command.Id)
                .Select(g => DepartmentServiceDto.FromEntity(g))
                .FirstOrDefaultAsync(cancellationToken);
            return new SuccessServiceResponse<DepartmentServiceDto>().WithData(result!);
        }
    }
}