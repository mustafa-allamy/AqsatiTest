using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class AddGeneralVacationTypesToDepartmentForm : ICommand<SuccessServiceResponse<List<DepartmentVacationTypeDto>>>
    {
        [JsonIgnore] public int DepartmentId { get; set; }
        public List<int> GeneralVacationTypeIds { get; set; }
    }
}