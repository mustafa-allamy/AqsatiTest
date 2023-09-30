using Application.CQRS.Permission.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;
using Persistence;

namespace Application.CQRS.Permission.Validations
{
    public class AddPermissionGroupToUserValidator : AbstractValidator<AddPermissionGroupToUserForm>
    {
        public AddPermissionGroupToUserValidator(ISharedCultureLocalizer localizer, IApplicationDbContext dbContext)
        {
            RuleFor(x => x.PermissionGroupId)
                .Must(x => dbContext.PermissionGroups.Any(y => y.Id == x))
                .WithMessage(localizer.GetLocalizedString("PermissionGroup.NotFound"));

            RuleFor(x => x.UserId)
                .Must(x => dbContext.Users.Any(y => y.Id == x))
                .WithMessage(localizer.GetLocalizedString("User.NotFound"));

            RuleFor(x => x)
                .Must(x => !dbContext.UserPermissionGroups.Any(y => y.PermissionGroupId == x.PermissionGroupId && y.UserId == x.UserId))
                .WithMessage(localizer.GetLocalizedString("UserPermissionGroup.Duplicated"));
        }
    }
}