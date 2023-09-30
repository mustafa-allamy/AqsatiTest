using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Common.Responses;
using Mediator;
using OneOf;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class GetDepartmentVacationTypeForm : IRequest<OneOf<SuccessServiceResponse<DepartmentVacationTypeDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
        [JsonIgnore] public int? DepartmentId { get; set; }
    }
}