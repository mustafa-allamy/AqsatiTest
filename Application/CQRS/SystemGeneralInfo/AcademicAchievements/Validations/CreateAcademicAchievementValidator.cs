using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Validations
{
    public class CreateAcademicAchievementValidator : AbstractValidator<CreateAcademicAchievementForm>
    {

        public CreateAcademicAchievementValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.AcademicAchievements.Any(y => y.Name.Equals(x)))
                .WithMessage("AcademicAchievement.Name.Unique");

            RuleFor(x => x.GeneralServiceId)
                .Must(x => dbContext.GeneralServices
                    .Where(y => y.Id == x && y.Type == Domain.Enums.ServiceType.Allowance)
                    .Include(y => y.ChildServices)
                    .Any(y => y.ParentServiceId > 0 || (((y.ParentServiceId ?? 0) == 0) && !y.ChildServices.Any()))
                ).When(x => x.GeneralServiceId is not null)
                .WithMessage("AcademicAchievement.GeneralServiceId.Unique");
        }
    }
}
