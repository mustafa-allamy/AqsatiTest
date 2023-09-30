using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Validators
{
    public class DeleteDepartmentVacationServiceRuleValidator : AbstractValidator<DeleteDepartmentVacationServiceRuleForm>
    {
        public DeleteDepartmentVacationServiceRuleValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.DepartmentVacationServiceRules.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");
        }
    }
}