

using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class DeleteDepartmentVacationTypeForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }
        [JsonIgnore] public int? DepartmentId { get; set; }

    }
}