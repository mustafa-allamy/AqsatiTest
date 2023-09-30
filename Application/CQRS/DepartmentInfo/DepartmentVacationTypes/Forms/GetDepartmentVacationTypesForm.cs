using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class GetDepartmentVacationTypesForm : BaseQuery, IRequest<SuccessServiceResponse<List<DepartmentVacationTypeDto>>>
    {
        public string? Name { get; set; }
        [JsonIgnore]
        [BindNever]
        public int? DepartmentId { get; set; }

    }
}