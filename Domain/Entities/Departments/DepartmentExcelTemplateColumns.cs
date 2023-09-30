using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentExcelTemplateColumns : BaseEntity<int>
    {
        public int DepartmentExcelTemplateId { get; set; }
        [ForeignKey(nameof(DepartmentExcelTemplateId))]
        public DepartmentExcelTemplate DepartmentExcelTemplate { get; set; }
        public int DefaultExcelTemplateColumnId { get; set; }
        [ForeignKey(nameof(DefaultExcelTemplateColumnId))]
        public DefaultExcelTemplateColumn DefaultExcelTemplateColumn { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int Order { get; set; }
    }
}