using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Enums;
using Mediator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Forms
{
    public class GetDepartmentServicesForm : BaseQuery, IRequest<SuccessServiceResponse<List<DepartmentServiceDto>>>
    {
        public string? Name { get; set; }
        public ServiceType Type { get; set; }
        public ServiceKind? Kind { get; set; }
        public bool? IsPercentage { get; set; }

        [JsonIgnore]
        [BindNever]
        public int? DepartmentId { get; set; }
    }
}