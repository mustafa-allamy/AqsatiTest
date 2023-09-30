using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Validations
{
    public class AddGeneralExcelTemplateServicesValidator : AbstractValidator<AddGeneralExcelTemplateServicesForm>
    {

        public AddGeneralExcelTemplateServicesValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.GeneralExcelTemplateId).NotEmpty()
                .Must(x => dbContext.GeneralExcelTemplates.Any(y => y.Id == x));

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
                .WithMessage("GeneralExcelTemplate.Services.DuplicateService");
        }

    }
}