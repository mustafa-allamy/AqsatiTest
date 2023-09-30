using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Validators
{
    public class DeleteDepartmentExcelTemplateValidator : AbstractValidator<DeleteDepartmentExcelTemplateForm>
    {
        public DeleteDepartmentExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));

            RuleFor(x => x.Id).NotEmpty().Must(x => dbContext.DepartmentExcelTemplates.Any(y => y.Id == x));

            RuleFor(x => x).Must(x =>
                dbContext.DepartmentExcelTemplates.Any(y => y.Id == x.Id && y.DepartmentId == x.DepartmentId))
                .When(x => x.DepartmentId is not null)
                .WithMessage("NoPermissionToAccessDepartment");
        }
    }
}