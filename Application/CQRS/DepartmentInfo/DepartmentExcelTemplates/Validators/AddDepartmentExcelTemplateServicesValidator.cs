using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Validators
{
    public class AddDepartmentExcelTemplateServicesValidator : AbstractValidator<AddDepartmentExcelTemplateServicesForm>
    {
        public AddDepartmentExcelTemplateServicesValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));

            RuleFor(x => x.DepartmentExcelTemplateId).NotEmpty()
                .Must(x => dbContext.DepartmentExcelTemplates.Any(y => y.Id == x));

            RuleFor(x => x.Services)
                //The ids of the added services must match the Department service
                .Must(x =>
                {
                    var departmentServices = dbContext.DepartmentServices.Select(y => y.Id).ToList();

                    foreach (var service in x)
                    {
                        if (!departmentServices.Contains(service.ServiceId))
                            return false;
                    }
                    return true;
                })
                .WithMessage("GeneralExcelTemplate.Services.WrongId")
                //The Added services must be main services only
                .Must(x =>
                {
                    var departmentServices = dbContext.DepartmentServices.Where(y => y.ParentServiceId == null && x.Select(z => z.ServiceId).Contains(y.Id)).Select(y => y.Id).Count();
                    return departmentServices == x.Count;
                })
                .WithMessage("GeneralExcelTemplate.Services.OnlyMainServices")
                .Must(x => !x.GroupBy(y => y.OrderNumber).Any(y => y.Count() > 1))
                .WithMessage("GeneralExcelTemplate.Services.DuplicateOrder")
                .Must(x => !x.GroupBy(y => y.ServiceId).Any(y => y.Count() > 1))
                .WithMessage("GeneralExcelTemplate.Services.DuplicateService");
        }
    }
}