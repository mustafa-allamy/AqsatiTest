using Application.CQRS.User.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.User.Validations
{
    public class AddRemoveUserUnitsValidator : AbstractValidator<AddRemoveUserUnitsForm>
    {
        public AddRemoveUserUnitsValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.UserId).NotEmpty().Must(x => dbContext.Users.Any(y => y.Id == x));
            RuleFor(x => x).NotEmpty().Must(x =>
            {
                var unitsCount =
                    dbContext.Units.Count(y => x.UnitsIds.Contains(y.Id) && y.DepartmentId == x.DepartmentId);

                return unitsCount == x.UnitsIds.Count;
            })
                .WithMessage("UserUnits.InvalidUnitId");
        }
    }
}