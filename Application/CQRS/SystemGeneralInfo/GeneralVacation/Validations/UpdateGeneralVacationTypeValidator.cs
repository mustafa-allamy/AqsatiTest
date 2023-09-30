using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Validations
{
    public class UpdateGeneralVacationTypeValidator : AbstractValidator<UpdateGeneralVacationTypeForm>
    {
        public UpdateGeneralVacationTypeValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.GeneralVacationTypes.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");

            RuleFor(x => x).NotEmpty()
                .Must(x => !dbContext.GeneralVacationTypes.Any(y => y.Name.Equals(x) && y.Id != x.Id))
                .WithMessage("GeneralVacationType.Name.Unique");


        }
    }
}