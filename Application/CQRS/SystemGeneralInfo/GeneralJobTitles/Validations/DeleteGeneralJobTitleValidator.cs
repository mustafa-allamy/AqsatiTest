using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Validations
{
    public class DeleteGeneralJobTitleValidator : AbstractValidator<DeleteGeneralJobTitleFrom>
    {
        public DeleteGeneralJobTitleValidator(IApplicationDbContext dbContext)
        {

            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralJobTitles.Any(y => y.Id == x))
                .WithMessage("GeneralJobTitle.NotFound");
        }
    }
}
