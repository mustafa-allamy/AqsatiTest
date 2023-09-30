using Common.Forms;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class AddGeneralExcelTemplateColumnForm : BaseForm<AddGeneralExcelTemplateColumnForm, GeneralExcelTemplateColumns>
    {
        public int DefaultExcelTemplateColumnId { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int Order { get; set; }
    }
}