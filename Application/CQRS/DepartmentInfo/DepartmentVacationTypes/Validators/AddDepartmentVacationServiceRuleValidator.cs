using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Domain.Enums;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Validators
{
    public class AddDepartmentVacationServiceRuleValidator : AbstractValidator<AddDepartmentVacationServiceRuleForm>
    {
        public AddDepartmentVacationServiceRuleValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.VacationTypeId).NotEmpty()
                .Must(x => dbContext.DepartmentVacationTypes.Any(y => y.Id == x));

            RuleFor(x => x.Amount).NotNull().InclusiveBetween(1, 100);

            //the rule allow only for parent services
            RuleFor(x => x.ServiceId).NotEmpty()
                .Must(x => dbContext.DepartmentServices.Any(y => y.Id == x && y.ParentServiceId == null))
                .WithMessage("VacationServiceRule.ServiceId");

            //the rule must be not duplicated for the same service
            RuleFor(x => x)
                .Must(x => !dbContext.DepartmentVacationServiceRules.Any(y =>
                    y.VacationTypeId == x.VacationTypeId && y.ServiceId == x.ServiceId))
                .WithMessage("VacationServiceRule.Duplicated");

            //the rule can't be added if the allowances or deductions are not allowed in vacation type
            RuleFor(x => x).Must(x =>
            {
                var serviceType = dbContext.DepartmentServices.First(y => y.Id == x.ServiceId);
                var vacationType = dbContext.DepartmentVacationTypes.First(y => y.Id == x.VacationTypeId);
                if (!vacationType.EffectedByAllAllowances && serviceType.Type == ServiceType.Allowance)
                    return false;
                if (!vacationType.EffectedByAllDeductions && serviceType.Type == ServiceType.Deduction)
                    return false;

                return true;

            }).WithMessage("VacationServiceRule.RuleNotAllowed");
        }
    }
}