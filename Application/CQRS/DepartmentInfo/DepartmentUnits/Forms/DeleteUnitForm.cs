using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Forms
{
    public class DeleteUnitForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }
        [JsonIgnore] public int? DepartmentId { get; set; }
    }
}