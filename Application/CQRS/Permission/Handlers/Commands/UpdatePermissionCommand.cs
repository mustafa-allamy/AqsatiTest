using Application.CQRS.Permission.Dtos;
using Application.CQRS.Permission.Forms;
using Common.Dto;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.Permission.Handlers.Commands
{
    public class UpdatePermissionCommand : ICommandHandler<UpdatePermissionForm, OneOf<SuccessServiceResponse<PermissionDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdatePermissionCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<OneOf<SuccessServiceResponse<PermissionDto>, FailServiceResponse>> Handle(UpdatePermissionForm request, CancellationToken cancellationToken)
        {
            var permission = await _dbContext.Permissions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (permission == null)
            {
                return new FailServiceResponse().WithError("Invalid permission id");
            }

            permission.UserDefinedName = request.UserDefinedName;
            _dbContext.Permissions.Update(permission);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SuccessServiceResponse<PermissionDto>().WithData(BaseDto<PermissionDto, Domain.Entities.UserAndPermissions.Permission>.FromEntity(permission));


        }
    }
}