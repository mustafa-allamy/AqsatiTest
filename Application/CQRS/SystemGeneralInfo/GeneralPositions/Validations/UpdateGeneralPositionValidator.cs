using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Validations
{
    public class UpdateGeneralPositionValidator : AbstractValidator<UpdateGeneralPositionForm>
    {

        public UpdateGeneralPositionValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralPositions.Any(y => y.Name.Equals(x.Name) && y.Id != x.Id))
                .WithMessage("GeneralPosition.Name.Unique");


            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralPositions.Any(y => y.Id == x))
                .WithMessage("GeneralPosition.NotFound");
        }
    }
}
