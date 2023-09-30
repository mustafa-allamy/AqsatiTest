using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Validations
{
    public class DeleteAcademicAchievementValidator : AbstractValidator<DeleteAcademicAchievementFrom>
    {
        public DeleteAcademicAchievementValidator(IApplicationDbContext dbContext)
        {

            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.AcademicAchievements.Any(y => y.Id == x))
                .WithMessage("AcademicAchievement.NotFound");


        }
    }
}
