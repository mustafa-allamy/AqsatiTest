using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Validators
{
    public class DeleteUnitValidator : AbstractValidator<DeleteUnitForm>
    {
        public DeleteUnitValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x).Must(x => dbContext.Units.Any(y => y.Id == x.Id && y.DepartmentId == x.DepartmentId))
                .WithMessage("ItemNotFound");

        }
    }
}