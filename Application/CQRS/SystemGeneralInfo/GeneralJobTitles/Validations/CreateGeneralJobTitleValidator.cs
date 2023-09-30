using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Validations
{
    public class CreateGeneralJobTitleValidator : AbstractValidator<CreateGeneralJobTitleForm>
    {

        public CreateGeneralJobTitleValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralJobTitles.Any(y => y.Name.Equals(x)))
                .WithMessage("GeneralJobTitle.Name.Unique");
        }
    }
}
