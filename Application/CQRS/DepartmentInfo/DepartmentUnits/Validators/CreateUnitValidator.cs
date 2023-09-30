using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Validators
{
    public class CreateUnitValidator : AbstractValidator<CreateUnitForm>
    {

        public CreateUnitValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));
            RuleFor(x => x.Name).NotEmpty();
            //Name must not be duplicated
            RuleFor(x => x).Must(x =>
                !dbContext.Units.Any(y => y.Name.Equals(x.Name) && x.DepartmentId == y.DepartmentId && x.ParentId == y.ParentId))
                .WithMessage("Unit.Duplicated");

        }
    }
}