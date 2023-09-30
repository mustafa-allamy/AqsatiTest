using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using FluentValidation;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Validators
{
    public class DeleteDepartmentVacationTypeValidator : AbstractValidator<DeleteDepartmentVacationTypeForm>
    {
        public DeleteDepartmentVacationTypeValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => dbContext.DepartmentVacationTypes.Any(y => y.Id == x))
                .WithMessage("ItemNotFound");



        }
    }
}