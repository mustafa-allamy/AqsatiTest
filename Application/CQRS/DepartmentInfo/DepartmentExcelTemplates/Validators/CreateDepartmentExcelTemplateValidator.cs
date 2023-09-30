using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Validators
{
    public class CreateDepartmentExcelTemplateValidator : AbstractValidator<CreateDepartmentExcelTemplateForm>
    {
        public CreateDepartmentExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));
            RuleFor(x => x).NotEmpty().Must(x => !dbContext.DepartmentExcelTemplates.Any(y => y.Name.Equals(x.Name) && x.DepartmentId == y.DepartmentId));
            RuleFor(x => x.Columns).NotEmpty()
                //The number of the added columns must be the same number of the default ones and all ids must match
                .Must(x =>
                {
                    var defaultColumns = dbContext.DefaultExcelTemplateColumns.Select(y => y.Id).ToList();
                    if (defaultColumns.Count != x.Count)
                        return false;

                    foreach (var column in x)
                    {
                        if (!defaultColumns.Contains(column.DefaultExcelTemplateColumnId))
                            return false;
                    }

                    return true;
                }).WithMessage("GeneralExcelTemplate.Column.MissingOrWrongId")
                //the order must not be duplicated
                .Must(x => !x.GroupBy(y => y.Order).Any(y => y.Count() > 1))
                .WithMessage("GeneralExcelTemplate.Column.DuplicateOrder");

            RuleFor(x => x)
                //The ids of the added services must match the general service
                .Must(x =>
                {
                    var services = dbContext.DepartmentServices.Select(y => y.Id).ToList();

                    foreach (var service in x.Services)
                    {
                        if (!services.Contains(service.ServiceId))
                            return false;
                    }
                    return true;
                })
                 .WithMessage("GeneralExcelTemplate.Services.WrongId")
                //The Added services must be main services only
                .Must(x =>
                {
                    var generalServices = dbContext.DepartmentServices.Where(y => y.ParentServiceId == null && y.DepartmentId == x.DepartmentId && x.Services.Select(z => z.ServiceId).Contains(y.Id)).Select(y => y.Id).Count();
                    return generalServices == x.Services.Count;
                })
                 .WithMessage("GeneralExcelTemplate.Services.OnlyMainServices")
                .Must(x => !x.Services.GroupBy(y => y.OrderNumber).Any(y => y.Count() > 1))
                 .WithMessage("GeneralExcelTemplate.Services.DuplicateOrder")
                .Must(x => !x.Services.GroupBy(y => y.ServiceId).Any(y => y.Count() > 1))
                 .WithMessage("GeneralExcelTemplate.Services.DuplicateService"); ;
        }
    }
}