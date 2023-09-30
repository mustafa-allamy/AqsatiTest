using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Validators
{
    public class UpdateDepartmentServiceValidator : AbstractValidator<UpdateDepartmentServiceForm>
    {
        public UpdateDepartmentServiceValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => dbContext.DepartmentServices.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");

            RuleFor(x => x.Amount).NotNull().ExclusiveBetween(0, 200).When(x => dbContext.DepartmentServices.First(y => y.Id == x.Id).IsPercentage).WithMessage("Service.PercentageLimit");
            RuleFor(x => x.Name).NotEmpty();
            //The service Name Must not be duplicated for the same type (different type,departments can have the same name only once)
            RuleFor(x => x).Must(x =>
                {
                    var service = dbContext.DepartmentServices.First(y => y.Id == x.Id);
                    return !dbContext.DepartmentServices.Any(y => y.Name.Equals(x.Name) && y.Type == service.Type && y.Id != x.Id && y.DepartmentId == service.DepartmentId);
                })
                .WithMessage("Service.DuplicatedName");

            RuleFor(x => x.Priority).NotEmpty();
        }
    }
}