using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Validators
{
    public class UpdateUnitValidator : AbstractValidator<UpdateUnitForm>
    {
        public UpdateUnitValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x).Must(x => dbContext.Units.Any(y => y.Id == x.Id && y.DepartmentId == x.DepartmentId)).WithMessage("ItemNotFound");

            RuleFor(x => x).Must(x => !dbContext.Units.Any(y => y.Name.Equals(x.Name) && y.DepartmentId == x.DepartmentId)).WithMessage("Unit.Duplicated");
        }
    }
}