using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Validations
{
    public class UpdateAcademicAchievementValidator : AbstractValidator<UpdateAcademicAchievementForm>
    {

        public UpdateAcademicAchievementValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.AcademicAchievements.Any(y => y.Name.Equals(x.Name) && y.Id != x.Id))
                .WithMessage("AcademicAchievement.Name.Unique");


            RuleFor(x => x.GeneralServiceId)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralServices
                    .Where(y => y.Id == x && y.Type == ServiceType.Allowance)
                    .Include(y => y.ChildServices)
                    .Any(y => y.ParentServiceId != null || (y.ParentServiceId == null && !y.ChildServices.Any()))
                ).When(x => x.GeneralServiceId is not null)
                .WithMessage("AcademicAchievement.GeneralServiceId");


            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.AcademicAchievements.Any(y => y.Id == x))
                .WithMessage("AcademicAchievement.NotFound");
        }
    }
}
