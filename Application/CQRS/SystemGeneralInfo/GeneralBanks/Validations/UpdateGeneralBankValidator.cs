using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Validations
{
    public class UpdateGeneralBankValidator : AbstractValidator<UpdateGeneralBankForm>
    {

        public UpdateGeneralBankValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.Name.Equals(x.Name) && y.Id != x.Id))
                .WithMessage("GeneralBank.Name.Unique");


            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.ReciverBIC.Equals(x.ReciverBIC) && y.Id != x.Id))
                .WithMessage("GeneralBank.ReciverBIC.Unique");



            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.BankCode.Equals(x.BankCode) && y.Id != x.Id))
                .WithMessage("GeneralBank.BankCode.Unique");



            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralBanks.Any(y => y.PayerAccount.Equals(x.PayerAccount) && y.Id != x.Id))
                .WithMessage("GeneralBank.PayerAccount.Unique");



            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralBanks.Any(y => y.Id == x))
                .WithMessage("GeneralBank.NotFound");
        }
    }
}
