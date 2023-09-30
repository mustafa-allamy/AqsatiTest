using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Validations
{
    public class DeleteGeneralVacationServiceRuleValidator : AbstractValidator<DeleteGeneralVacationServiceRuleForm>
    {
        public DeleteGeneralVacationServiceRuleValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.GeneralVacationServiceRules.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");
        }
    }
}