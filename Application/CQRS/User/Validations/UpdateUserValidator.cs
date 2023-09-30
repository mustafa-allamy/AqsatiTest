using Application.CQRS.User.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;
using Persistence;

namespace Application.CQRS.User.Validations
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserForm>
    {
        public UpdateUserValidator(ISharedCultureLocalizer localizer, IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .Must(x =>
                {
                    return dbContext.Users.Any(y => y.Id == x);
                }).WithMessage(localizer.GetLocalizedString("User.NotFound"));
            RuleFor(register => register.FirstName)
                .MinimumLength(3).When(x => x.FirstName is not null)
                .WithMessage(localizer.GetLocalizedString("User.FirstName.Required"));

            RuleFor(register => register.MiddleName)
                .MinimumLength(3).When(x => x.MiddleName is not null)
                .WithMessage(localizer.GetLocalizedString("User.MiddleName.Required"));

            RuleFor(register => register.LastName)
                .MinimumLength(3).When(x => x.LastName is not null)
                .WithMessage(localizer.GetLocalizedString("User.LastName.Required"));

            RuleFor(register => register.PhoneNumber).Matches("07[3-9][0-9]{8}").WithMessage(localizer.GetLocalizedString("User.PhoneNumber.Incorrect"));

            RuleFor(p => p.Password)
                .MinimumLength(6).WithMessage(localizer.GetLocalizedString("User.Password.MinimumLength"))
                .MaximumLength(16).WithMessage(localizer.GetLocalizedString("User.Password.MaxLength"))
                .Matches(@"[A-Z]+").WithMessage(localizer.GetLocalizedString("User.Password.CharactersType"))
                .Matches(@"[a-z]+").WithMessage(localizer.GetLocalizedString("User.Password.CharactersType"))
                .Matches(@"[0-9]+").WithMessage(localizer.GetLocalizedString("User.Password.CharactersType"));
        }
    }
}