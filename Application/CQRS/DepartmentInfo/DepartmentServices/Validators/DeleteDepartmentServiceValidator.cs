using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Validators
{
    public class DeleteDepartmentServiceValidator : AbstractValidator<DeleteDepartmentServiceForm>
    {
        public DeleteDepartmentServiceValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty().Must(x => dbContext.Departments.Any(y => y.Id == x));

            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.DepartmentServices.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");

            RuleFor(x => x).Must(x =>
                    !dbContext.DepartmentServices.Include(y => y.ChildServices).Any(y => y.Id == x.Id && y.ChildServices.Any()))
                .WithMessage("GeneralService.ContainsChild");


            RuleFor(x => x).Must(x =>
                    !dbContext.DepartmentExcelTemplateServices.Any(y => y.ServiceId == x.Id))
                .WithMessage("GeneralService.UsedInExcelTemplate");
        }
    }
}