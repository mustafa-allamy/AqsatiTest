using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Validators
{
    public class CreateDepartmentServiceValidator : AbstractValidator<CreateDepartmentServiceForm>
    {
        public CreateDepartmentServiceValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));
            RuleFor(x => x.Amount).NotNull().ExclusiveBetween(0, 200).When(x => x.IsPercentage).WithMessage("Service.PercentageLimit");
            RuleFor(x => x.IsPercentage).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Type).NotNull();
            //The service Name Must not be duplicated for the same type (different type can have the same name only once)
            RuleFor(x => x).Must(x =>
                {
                    return !dbContext.DepartmentServices.Any(y => y.Name.Equals(x.Name) && y.Type == x.Type && x.DepartmentId == y.DepartmentId);
                })
                .WithMessage("Service.DuplicatedName");
            //The parent service should not be child it self
            RuleFor(x => x.ParentServiceId)
                .Must(x => dbContext.DepartmentServices.Any(y => y.Id == x && y.ParentServiceId == null))
                .When(x => x.ParentServiceId != null)
                .WithMessage("Service.ParentServiceCantBeChild");

            RuleFor(x => x.Priority).NotEmpty();
        }
    }
}