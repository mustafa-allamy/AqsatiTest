using Application.CQRS.Permission.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;
using Persistence;

namespace Application.CQRS.Permission.Validations
{
    public class UpdatePermissionGroupValidator : AbstractValidator<UpdatePermissionGroupForm>
    {
        public UpdatePermissionGroupValidator(ISharedCultureLocalizer localizer, IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.PermissionGroups.Any(y => y.Id == x))
                .WithMessage(localizer.GetLocalizedString("PermissionGroup.NotFound"));

            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.PermissionGroups.Any(y => y.Name.Equals(x.Name) && y.Id != x.Id))
                .WithMessage(localizer.GetLocalizedString("PermissionGroup.Name.Unique"));
        }
    }
}