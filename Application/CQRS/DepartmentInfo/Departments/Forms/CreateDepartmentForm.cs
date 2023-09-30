using Application.CQRS.DepartmentInfo.Departments.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using OneOf;

namespace Application.CQRS.DepartmentInfo.Departments.Forms
{
    public class CreateDepartmentForm : BaseForm<CreateDepartmentForm, Department>, ICommand<OneOf<SuccessServiceResponse<DepartmentDto>, FailServiceResponse>>
    {
        public int MinistryId { get; set; }

        public string Name { get; set; }
        public string Domain { get; set; }


    }
}