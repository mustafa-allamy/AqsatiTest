using Common.Forms;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class UpdateDepartmentExcelTemplateServiceForm : BaseForm<UpdateDepartmentExcelTemplateServiceForm, DepartmentExcelTemplateService>
    {
        public int Id { get; set; }
        public string? AlternativeName { get; set; }
        public bool? IsVisible { get; set; } = true;

        public int? OrderNumber { get; set; }
        public bool? ShowChildService { get; set; } = false;
    }
}