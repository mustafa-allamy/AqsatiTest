using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class AddDepartmentExcelTemplateServicesForm : ICommand<SuccessServiceResponse<List<DepartmentExcelTemplateServiceDto>>>
    {
        [JsonIgnore]
        public int DepartmentExcelTemplateId { get; set; }
        [JsonIgnore] public int? DepartmentId { get; set; }

        public List<AddDepartmentExcelTemplateServiceForm> Services { get; set; }
    }
}