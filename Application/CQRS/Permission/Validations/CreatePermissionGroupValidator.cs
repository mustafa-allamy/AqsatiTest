using Application.CQRS.Permission.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;
using Persistence;

namespace Application.CQRS.Permission.Validations
{
    public class CreatePermissionGroupValidator : AbstractValidator<CreatePermissionGroupForm>
    {

        public CreatePermissionGroupValidator(ISharedCultureLocalizer localizer, IApplicationDbContext dbContext)
        {

            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.PermissionGroups.Any(y => y.Name.Equals(x)))
                .WithMessage(localizer.GetLocalizedString("PermissionGroup.Name.Unique"));

            RuleFor(x => x.PermissionsIds)
                .Must(x =>
                {
                    int count = dbContext.Permissions.Count(y => x.Contains(y.Id));
                    return count == x.Count;
                }).WithMessage(localizer.GetLocalizedString("Permission.NotFound"));
        }
    }
}