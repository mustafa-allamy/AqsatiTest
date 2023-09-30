using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Validations
{
    public class DeleteGeneralPositionValidator : AbstractValidator<DeleteGeneralPositionFrom>
    {
        public DeleteGeneralPositionValidator(IApplicationDbContext dbContext)
        {

            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralPositions.Any(y => y.Id == x))
                .WithMessage("GeneralPosition.NotFound");
        }
    }
}
