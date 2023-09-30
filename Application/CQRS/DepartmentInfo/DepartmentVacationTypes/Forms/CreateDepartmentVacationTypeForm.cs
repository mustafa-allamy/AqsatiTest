using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class CreateDepartmentVacationTypeForm : BaseForm<CreateDepartmentVacationTypeForm, DepartmentVacationType>, ICommand<SuccessServiceResponse<DepartmentVacationTypeDto>>
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool EffectedByAllAllowances { get; set; }
        public bool EffectedByAllDeductions { get; set; }

        [JsonIgnore] public int? DepartmentId { get; set; }
    }
}