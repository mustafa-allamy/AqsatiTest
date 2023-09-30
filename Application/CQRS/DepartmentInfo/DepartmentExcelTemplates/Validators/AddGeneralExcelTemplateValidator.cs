using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Validators
{
    public class AddGeneralExcelTemplateValidator : AbstractValidator<AddGeneralExcelTemplatesToDepartmentForm>
    {
        public AddGeneralExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));
            RuleFor(x => x.GeneralExcelTemplateIds).NotEmpty().Must(x =>
            {
                var count = dbContext.GeneralExcelTemplates.Count(y => x.Contains(y.Id));
                return count == x.Count;
            }).WithMessage("DepartmentExcelTemplate.InvalidGeneralTemplateId");
        }
    }
}