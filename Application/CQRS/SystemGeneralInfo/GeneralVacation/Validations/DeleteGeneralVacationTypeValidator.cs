using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Validations
{
    public class DeleteGeneralVacationTypeValidator : AbstractValidator<DeleteGeneralVacationTypeForm>
    {
        public DeleteGeneralVacationTypeValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.GeneralVacationTypes.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");

            RuleFor(x => x)
                .Must(x => !dbContext.DepartmentVacationTypes.Any(y => y.GeneralVacationTypeId == x.Id))
                .WithMessage("GeneralVacationType.UsedInDepartment");

        }
    }
}