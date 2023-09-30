using Common.Forms;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class AddGeneralExcelTemplateServiceForm : BaseForm<AddGeneralExcelTemplateServiceForm, GeneralExcelTemplateService>
    {
        public int ServiceId { get; set; }

        public string? AlternativeName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int OrderNumber { get; set; }
        public bool ShowChildService { get; set; } = false;
    }
}