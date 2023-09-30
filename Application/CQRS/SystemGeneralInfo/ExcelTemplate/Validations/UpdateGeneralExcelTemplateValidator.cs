using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Validations
{
    public class UpdateGeneralExcelTemplateValidator : AbstractValidator<UpdateGeneralExcelTemplateForm>
    {
        public UpdateGeneralExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x).Must(x => !dbContext.GeneralExcelTemplates.Any(y => y.Name.Equals(x.Name) && x.Id != y.Id));
            RuleFor(x => x)
                //The updated columns must match the number and ids of existing columns
                .Must(x =>
            {
                var generalColumns = dbContext.GeneralExcelTemplateColumns.Where(y => y.GeneralExcelTemplateId == x.Id).Select(y => y.Id).ToList();
                if (generalColumns.Count != x.Columns.Count)
                    return false;

                foreach (var column in x.Columns)
                {
                    if (!generalColumns.Contains(column.Id))
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
                    var generalServices = dbContext.GeneralExcelTemplateServices.Where(y => y.GeneralExcelTemplateId == x.Id).Select(y => y.Id).ToList();
                    if (generalServices.Count != x.Services.Count)
                        return false;

                    foreach (var service in x.Services)
                    {
                        if (!generalServices.Contains(service.Id))
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