using Application.CQRS.Permission.Forms;
using FluentValidation;
using LazZiya.ExpressLocalization;
using Persistence;

namespace Application.CQRS.Permission.Validations
{
    public class DeletePermissionGroupValidator : AbstractValidator<DeletePermissionGroupFrom>
    {
        public DeletePermissionGroupValidator(ISharedCultureLocalizer localizer, IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => !dbContext.UserPermissionGroups.Any(y => y.PermissionGroupId == x))
                .WithMessage(localizer.GetLocalizedString("PermissionGroup.NotFound"));
        }
    }
}