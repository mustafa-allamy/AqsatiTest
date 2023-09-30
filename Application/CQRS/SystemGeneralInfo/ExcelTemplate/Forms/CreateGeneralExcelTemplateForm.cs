using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class CreateGeneralExcelTemplateForm : BaseForm<CreateGeneralExcelTemplateForm, GeneralExcelTemplate>, ICommand<SuccessServiceResponse<GeneralExcelTemplateDto>>
    {
        public string Name { get; set; }
        public List<AddGeneralExcelTemplateColumnForm> Columns { get; set; }
        public List<AddGeneralExcelTemplateServiceForm> Services { get; set; }
    }
}