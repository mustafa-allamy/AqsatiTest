using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class UpdateDepartmentExcelTemplateForm : ICommand<SuccessServiceResponse<DepartmentExcelTemplateDto>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }

        public List<UpdateDepartmentExcelTemplateColumnForm> Columns { get; set; }
        public List<UpdateDepartmentExcelTemplateServiceForm> Services { get; set; }
    }
}