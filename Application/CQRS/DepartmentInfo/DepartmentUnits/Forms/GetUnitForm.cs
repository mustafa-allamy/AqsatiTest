using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Common.Responses;
using Mediator;
using OneOf;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Forms
{
    public class GetUnitForm : IRequest<OneOf<SuccessServiceResponse<UnitDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
        [JsonIgnore] public int? DepartmentId { get; set; }
    }
}