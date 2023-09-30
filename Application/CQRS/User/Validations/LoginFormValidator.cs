using Application.CQRS.User.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;

namespace Application.CQRS.User.Validations
{
    internal class LoginFormValidator : AbstractValidator<LoginForm>
    {
        public LoginFormValidator(ISharedCultureLocalizer _localizer)
        {
            RuleFor(x => x.Username).EmailAddress().WithMessage(_localizer.GetLocalizedString("Login.Username.NotValid"));
        }
    }
}
