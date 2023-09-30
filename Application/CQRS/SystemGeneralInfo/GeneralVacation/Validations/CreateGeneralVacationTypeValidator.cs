using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Validations
{
    public class CreateGeneralVacationTypeValidator : AbstractValidator<CreateGeneralVacationTypeForm>
    {
        public CreateGeneralVacationTypeValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Amount).NotNull().ExclusiveBetween(0, 100);
            RuleFor(x => x.Name).NotEmpty()
                .Must(x => !dbContext.GeneralVacationTypes.Any(y => y.Name.Equals(x)))
                .WithMessage("GeneralVacationType.Name.Unique");

        }
    }
}