using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Responses;
using Mediator;
using OneOf;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Forms
{
    public class GetDepartmentServiceForm : IRequest<OneOf<SuccessServiceResponse<DepartmentServiceDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int? DepartmentId { get; set; }
    }
}