using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Forms
{
    public class AddDepartmentServicesForm : ICommand<SuccessServiceResponse<List<DepartmentServiceDto>>>
    {
        [JsonIgnore]
        public int DepartmentId { get; set; }
        public List<int> GeneralServicesIds { get; set; }
    }
}