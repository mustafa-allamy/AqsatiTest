using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.Departments;
using Domain.Enums;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Forms
{
    public class CreateDepartmentServiceForm : BaseForm<CreateDepartmentServiceForm, DepartmentService>, ICommand<SuccessServiceResponse<DepartmentServiceDto>>
    {

        public string Name { get; set; }
        public double Amount { get; set; }
        public int? ParentServiceId { get; set; }
        public ServiceKind Kind { get; set; }
        public bool IsPercentage { get; set; }
        public ServiceType Type { get; set; }
        public int Priority { get; set; }

        [JsonIgnore]
        public int? DepartmentId { get; set; }

    }
}