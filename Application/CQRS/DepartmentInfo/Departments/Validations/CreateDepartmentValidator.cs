using Application.CQRS.DepartmentInfo.Departments.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.Departments.Validations
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentForm>
    {
        public CreateDepartmentValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.MinistryId).NotEmpty()
                .Must(x => dbContext.Ministries.Any(y => y.Id == x));

            RuleFor(x => x.Domain).NotEmpty()
                .Must(x => !dbContext.Departments.Any(y => y.Domain == x))
                .WithMessage("Department.Domain.Duplicated");
            RuleFor(x => x.Name).NotEmpty()
                .Must(x => !dbContext.Departments.Any(y => y.Name == x))
                .WithMessage("Department.Name.Duplicated");
        }
    }
}