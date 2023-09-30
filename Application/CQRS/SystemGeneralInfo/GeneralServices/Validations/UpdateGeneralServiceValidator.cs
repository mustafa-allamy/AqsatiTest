using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Validations
{
    public class UpdateGeneralServiceValidator : AbstractValidator<UpdateGeneralServiceForm>
    {
        public UpdateGeneralServiceValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.GeneralServices.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");

            RuleFor(x => x.Amount).NotNull().ExclusiveBetween(0, 200).When(x => dbContext.GeneralServices.First(y => y.Id == x.Id).IsPercentage).WithMessage("Service.PercentageLimit");
            RuleFor(x => x.Name).NotEmpty();
            //The service Name Must not be duplicated for the same type (different type can have the same name only once)
            RuleFor(x => x).Must(x =>
                {
                    var currentType = dbContext.GeneralServices.First(y => y.Id == x.Id).Type;
                    return !dbContext.GeneralServices.Any(y => y.Name.Equals(x.Name) && y.Type == currentType && y.Id != x.Id);
                })
                .WithMessage("Service.DuplicatedName");

            RuleFor(x => x.Priority).NotEmpty();
        }
    }
}