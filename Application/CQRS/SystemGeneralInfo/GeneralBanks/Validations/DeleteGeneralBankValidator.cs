using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Validations
{
    public class DeleteGeneralBankValidator : AbstractValidator<DeleteGeneralBankFrom>
    {
        public DeleteGeneralBankValidator(IApplicationDbContext dbContext)
        {

            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralBanks.Any(y => y.Id == x))
                .WithMessage("GeneralBank.NotFound");

        }
    }
}
