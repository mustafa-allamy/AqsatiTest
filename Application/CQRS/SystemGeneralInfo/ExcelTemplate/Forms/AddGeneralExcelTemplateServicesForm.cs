using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class AddGeneralExcelTemplateServicesForm : ICommand<SuccessServiceResponse<List<GeneralExcelTemplateServiceDto>>>
    {
        [JsonIgnore]
        public int GeneralExcelTemplateId { get; set; }

        public List<AddGeneralExcelTemplateServiceForm> Services { get; set; }
    }
}