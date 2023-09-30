using Application.CQRS.Permission.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;
using Persistence;

namespace Application.CQRS.Permission.Validations
{
    public class UpdateUserPermissionsValidator : AbstractValidator<UpdateUserPermissionsForm>
    {
        public UpdateUserPermissionsValidator(ISharedCultureLocalizer localizer, IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.Users.Any(y => y.Id == x))
                .WithMessage(localizer.GetLocalizedString("User.NotFound"));

            RuleFor(x => x.PermissionsIds)
                .Must(x =>
                {
                    int count = dbContext.Permissions.Count(y => x.Contains(y.Id));
                    return count == x.Count;
                }).WithMessage(localizer.GetLocalizedString("Permission.NotFound"));
        }
    }
}