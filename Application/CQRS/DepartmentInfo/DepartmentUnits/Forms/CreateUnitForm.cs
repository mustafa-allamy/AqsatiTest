using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;
using Unit = Domain.Entities.Departments.Unit;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Forms
{
    public class CreateUnitForm : BaseForm<CreateUnitForm, Unit>, ICommand<SuccessServiceResponse<UnitDto>>
    {
        [JsonIgnore]
        public int? DepartmentId { get; set; }

        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}