using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class UpdateGeneralExcelTemplateForm : ICommand<SuccessServiceResponse<GeneralExcelTemplateDto>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<UpdateGeneralExcelTemplateColumnForm> Columns { get; set; }
        public List<UpdateGeneralExcelTemplateServiceForm> Services { get; set; }
    }
}