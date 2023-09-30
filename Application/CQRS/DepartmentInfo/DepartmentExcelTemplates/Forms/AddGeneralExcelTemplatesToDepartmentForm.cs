using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class AddGeneralExcelTemplatesToDepartmentForm : ICommand<SuccessServiceResponse<List<DepartmentExcelTemplateDto>>>
    {
        [JsonIgnore]
        public int DepartmentId { get; set; }
        public List<int> GeneralExcelTemplateIds { get; set; }
    }
}