using Application.CQRS.User.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;

namespace Application.CQRS.User.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterForm>
    {

        public RegisterValidator(ISharedCultureLocalizer localizer)
        {
            RuleFor(register => register.FirstName).NotNull().NotEmpty().WithMessage(localizer.GetLocalizedString("User.FirstName.Required"));
            RuleFor(register => register.MiddleName).NotNull().NotEmpty().WithMessage(localizer.GetLocalizedString("User.MiddleName.Required"));
            RuleFor(register => register.LastName).NotNull().NotEmpty().WithMessage(localizer.GetLocalizedString("User.LastName.Required"));
            RuleFor(register => register.PhoneNumber).NotNull().NotEmpty().Matches("07[3-9][0-9]{8}").WithMessage(localizer.GetLocalizedString("User.PhoneNumber.Incorrect"));
            RuleFor(p => p.Password).NotEmpty()
                .MinimumLength(6).WithMessage(localizer.GetLocalizedString("User.Password.MinimumLength"))
                .MaximumLength(16).WithMessage(localizer.GetLocalizedString("User.Password.MaxLength"))
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+")
                .WithMessage(localizer.GetLocalizedString("User.Password.CharactersType"));
        }
    }
}