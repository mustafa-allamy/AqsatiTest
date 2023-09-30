using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Validators
{
    public class CreateDepartmentVacationTypeValidator : AbstractValidator<CreateDepartmentVacationTypeForm>
    {
        public CreateDepartmentVacationTypeValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));
            RuleFor(x => x.Amount).NotNull().ExclusiveBetween(0, 100);
            RuleFor(x => x).NotEmpty()
                .Must(x => !dbContext.DepartmentVacationTypes.Any(y => y.Name.Equals(x.Name) && x.DepartmentId == y.DepartmentId))
                .WithMessage("GeneralVacationType.Name.Unique");
        }
    }
}