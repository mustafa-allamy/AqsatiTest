using Common.Forms;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class AddDepartmentExcelTemplateColumnForm : BaseForm<AddDepartmentExcelTemplateColumnForm, DepartmentExcelTemplateColumns>
    {
        public int DefaultExcelTemplateColumnId { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int Order { get; set; }
    }
}