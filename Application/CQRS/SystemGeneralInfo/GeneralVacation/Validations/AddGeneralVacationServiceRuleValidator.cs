using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Domain.Enums;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Validations
{
    public class AddGeneralVacationServiceRuleValidator : AbstractValidator<AddGeneralVacationServiceRuleForm>
    {
        public AddGeneralVacationServiceRuleValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.VacationTypeId).NotEmpty()
                .Must(x => dbContext.GeneralVacationTypes.Any(y => y.Id == x));

            RuleFor(x => x.Amount).NotNull().InclusiveBetween(1, 100);

            //the rule allowed only for parent services
            RuleFor(x => x.ServiceId).NotEmpty()
                .Must(x => dbContext.GeneralServices.Any(y => y.Id == x && y.ParentServiceId == null))
                .WithMessage("VacationServiceRule.ServiceId");

            //the rule must be not duplicated for the same service
            RuleFor(x => x)
                .Must(x => !dbContext.GeneralVacationServiceRules.Any(y =>
                    y.VacationTypeId == x.VacationTypeId && y.ServiceId == x.ServiceId))
                .WithMessage("VacationServiceRule.Duplicated");

            //the rule can't be added if the allowances or deductions are not allowed in vacation type
            RuleFor(x => x).Must(x =>
            {
                var serviceType = dbContext.GeneralServices.First(y => y.Id == x.ServiceId);
                var vacationType = dbContext.GeneralVacationTypes.First(y => y.Id == x.VacationTypeId);
                if (!vacationType.EffectedByAllAllowances && serviceType.Type == ServiceType.Allowance)
                    return false;
                if (!vacationType.EffectedByAllDeductions && serviceType.Type == ServiceType.Deduction)
                    return false;

                return true;

            }).WithMessage("VacationServiceRule.RuleNotAllowed");
        }
    }
}