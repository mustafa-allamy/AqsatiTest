using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Validations
{
    public class CreateGeneralBankValidator : AbstractValidator<CreateGeneralBankForm>
    {

        public CreateGeneralBankValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.Name.Equals(x)))
                .WithMessage("GeneralBank.Name.Unique");


            RuleFor(x => x.ReciverBIC)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.ReciverBIC.Equals(x)))
                .WithMessage("GeneralBank.ReciverBIC.Unique");



            RuleFor(x => x.BankCode)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.BankCode.Equals(x)))
                .WithMessage("GeneralBank.BankCode.Unique");



            RuleFor(x => x.PayerAccount)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.PayerAccount.Equals(x)))
                .WithMessage("GeneralBank.PayerAccount.Unique");



        }
    }
}
