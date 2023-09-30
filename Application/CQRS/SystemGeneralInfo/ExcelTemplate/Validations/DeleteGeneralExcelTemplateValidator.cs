using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Validations
{
    public class DeleteGeneralExcelTemplateValidator : AbstractValidator<DeleteGeneralExcelTemplateForm>
    {
        public DeleteGeneralExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => dbContext.GeneralExcelTemplates.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");

            RuleFor(x => x.Id)
                .Must(x => !dbContext.DepartmentExcelTemplates.Any(y => y.GeneralExcelTemplateId == x))
                .WithMessage("GeneralExcelTemplate.DeleteNotAllowed");
        }
    }
}