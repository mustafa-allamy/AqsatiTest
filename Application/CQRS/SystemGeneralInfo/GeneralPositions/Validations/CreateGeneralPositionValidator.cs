using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Validations
{
    public class CreateGeneralPositionValidator : AbstractValidator<CreateGeneralPositionForm>
    {

        public CreateGeneralPositionValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralPositions.Any(y => y.Name.Equals(x)))
                .WithMessage("GeneralPosition.Name.Unique");

            RuleFor(x => x.GeneralServiceId)
                .Must(x => dbContext.GeneralServices
                    .Where(y => y.Id == x && y.Type == Domain.Enums.ServiceType.Allowance)
                    .Include(y => y.ChildServices)
                    .Any(y => y.ParentServiceId != null || (y.ParentServiceId == null && !y.ChildServices.Any()))
                ).When(x => x.GeneralServiceId is not null)
                .WithMessage("GeneralPosition.GeneralService");
        }
    }
}
