using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Forms
{
    public class GetUnitsForm : BaseQuery, IRequest<SuccessServiceResponse<List<UnitDto>>>
    {
        [JsonIgnore]
        [BindNever]
        public int? DepartmentId { get; set; }
    }
}