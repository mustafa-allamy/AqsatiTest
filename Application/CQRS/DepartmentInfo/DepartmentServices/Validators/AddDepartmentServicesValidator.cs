using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Validators
{
    public class AddDepartmentServicesValidator : AbstractValidator<AddDepartmentServicesForm>
    {
        public AddDepartmentServicesValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty()
                .Must(x => dbContext.Departments.Any(y => y.Id == x));

            RuleFor(x => x.GeneralServicesIds).Must(x =>
            {
                var servicesCount = dbContext.GeneralServices.Count(y => x.Contains(y.Id) && y.ParentServiceId == null);

                return servicesCount == x.Count;
            }).WithMessage("DepartmentServices.Add.OnlyMainGeneralServices");

        }
    }
}