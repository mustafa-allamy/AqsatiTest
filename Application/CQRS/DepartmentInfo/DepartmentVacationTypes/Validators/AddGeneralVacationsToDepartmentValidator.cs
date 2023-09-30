using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Validators
{
    public class AddGeneralVacationsToDepartmentValidator : AbstractValidator<AddGeneralVacationTypesToDepartmentForm>
    {

        public AddGeneralVacationsToDepartmentValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));
            RuleFor(x => x.GeneralVacationTypeIds).NotEmpty().Must(x =>
            {
                var count = dbContext.GeneralExcelTemplates.Count(y => x.Contains(y.Id));
                return count == x.Count;
            }).WithMessage("DepartmentVacationType.InvalidGeneralVacationTypeId");
        }
    }
}