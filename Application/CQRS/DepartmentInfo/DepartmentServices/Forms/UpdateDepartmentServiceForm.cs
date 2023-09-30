using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.Departments;
using Domain.Enums;
using Mediator;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Forms
{
    public class UpdateDepartmentServiceForm : BaseForm<UpdateDepartmentServiceForm, DepartmentService>, ICommand<SuccessServiceResponse<DepartmentServiceDto>>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public ServiceKind Kind { get; set; }
        public int Priority { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int? DepartmentId { get; set; }

    }
}