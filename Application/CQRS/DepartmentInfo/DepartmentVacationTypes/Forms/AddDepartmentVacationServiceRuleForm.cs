using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class AddDepartmentVacationServiceRuleForm : BaseForm<AddDepartmentVacationServiceRuleForm, DepartmentVacationServiceRule>, ICommand<SuccessServiceResponse<DepartmentVacationServiceRuleDto>>
    {
        [JsonIgnore]
        public int VacationTypeId { get; set; }

        public int ServiceId { get; set; }
        public bool NotEffectedByBasicSalaryDeduction { get; set; }
        public int Amount { get; set; }
    }
}