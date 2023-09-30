using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class GetDepartmentExcelTemplatesForm : BaseQuery, IRequest<SuccessServiceResponse<List<DepartmentExcelTemplateDto>>>
    {
        [JsonIgnore]
        [BindNever]
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }

    }
}