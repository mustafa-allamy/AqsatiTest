using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Validations
{
    public class UpdateGeneralJobTitleValidator : AbstractValidator<UpdateGeneralJobTitleForm>
    {

        public UpdateGeneralJobTitleValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x)
                .NotNull().NotEmpty()
                .Must(x => !dbContext.GeneralJobTitles.Any(y => y.Name.Equals(x.Name) && y.Id != x.Id))
                .WithMessage("GeneralJobTitle.Name.Unique");


            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralJobTitles.Any(y => y.Id == x))
                .WithMessage("GeneralJobTitle.NotFound");
        }
    }
}
