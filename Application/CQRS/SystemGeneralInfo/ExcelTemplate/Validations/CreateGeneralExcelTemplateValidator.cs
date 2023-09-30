using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Validations
{
    public class CreateGeneralExcelTemplateValidator : AbstractValidator<CreateGeneralExcelTemplateForm>
    {

        public CreateGeneralExcelTemplateValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().Must(x => !dbContext.GeneralExcelTemplates.Any(y => y.Name.Equals(x)));
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

            RuleFor(x => x.Services)
                //The ids of the added services must match the general service
                .Must(x =>
                {
                    var generalServices = dbContext.GeneralServices.Select(y => y.Id).ToList();

                    foreach (var service in x)
                    {
                        if (!generalServices.Contains(service.ServiceId))
                            return false;
                    }
                    return true;
                })
                 .WithMessage("GeneralExcelTemplate.Services.WrongId")
                //The Added services must be main services only
                .Must(x =>
                {
                    var generalServices = dbContext.GeneralServices.Where(y => y.ParentServiceId == null && x.Select(z => z.ServiceId).Contains(y.Id)).Select(y => y.Id).Count();
                    return generalServices == x.Count;
                })
                 .WithMessage("GeneralExcelTemplate.Services.OnlyMainServices")
                .Must(x => !x.GroupBy(y => y.OrderNumber).Any(y => y.Count() > 1))
                 .WithMessage("GeneralExcelTemplate.Services.DuplicateOrder")
                .Must(x => !x.GroupBy(y => y.ServiceId).Any(y => y.Count() > 1))
                 .WithMessage("GeneralExcelTemplate.Services.DuplicateService"); ;
        }
    }
}