using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Validators
{
    public class UpdateDepartmentExcelTemplateValidator : AbstractValidator<UpdateDepartmentExcelTemplateForm>
    {
        public UpdateDepartmentExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));

            RuleFor(x => x).Must(x => !dbContext.DepartmentExcelTemplates.Any(y => y.Name.Equals(x.Name) && x.Id != y.Id && y.DepartmentId == x.DepartmentId));
            RuleFor(x => x)
                //The updated columns must match the number and ids of existing columns
                .Must(x =>
                {
                    var columns = dbContext.DepartmentExcelTemplateColumns.Where(y => y.DepartmentExcelTemplateId == x.Id).Select(y => y.Id).ToList();
                    if (columns.Count != x.Columns.Count)
                        return false;

                    foreach (var column in x.Columns)
                    {
                        if (!columns.Contains(column.Id))
                            return false;
                    }

                    return true;
                }).WithMessage("GeneralExcelTemplate.Column.MissingOrWrongId")
                .Must(x => !x.Columns.GroupBy(y => y.Order).Any(y => y.Count() > 1))
                .WithMessage("GeneralExcelTemplate.Column.DuplicateOrder");

            RuleFor(x => x)
                //The updated Services must match the number and ids of existing Services
                .Must(x =>
                {
                    var services = dbContext.DepartmentExcelTemplateServices.Where(y => y.DepartmentExcelTemplateId == x.Id).Select(y => y.Id).ToList();
                    if (services.Count != x.Services.Count)
                        return false;

                    foreach (var service in x.Services)
                    {
                        if (!services.Contains(service.Id))
                            return false;
                    }
                    return true;
                })
                 .WithMessage("GeneralExcelTemplate.Services.WrongId")
                .Must(x => !x.Services.GroupBy(y => y.OrderNumber).Any(y => y.Count() > 1))
                 .WithMessage("GeneralExcelTemplate.Services.DuplicateOrder");
        }
    }
}