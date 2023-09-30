using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Validations
{
    public class DeleteGeneralServiceValidator : AbstractValidator<DeleteGeneralServiceForm>
    {
        public DeleteGeneralServiceValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.GeneralServices.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");
            RuleFor(x => x).Must(x =>
                    !dbContext.GeneralServices.Include(y => y.ChildServices).Any(y => y.Id == x.Id && y.ChildServices.Any()))
                .WithMessage("GeneralService.ContainsChild");

            RuleFor(x => x).Must(x =>
                    !dbContext.DepartmentServices.Any(y => y.GeneralServiceId == x.Id))
                .WithMessage("GeneralService.UsedByDepartment");

            RuleFor(x => x).Must(x =>
                    !dbContext.GeneralExcelTemplateServices.Any(y => y.ServiceId == x.Id))
                .WithMessage("GeneralService.UsedInExcelTemplate");
        }
    }
}