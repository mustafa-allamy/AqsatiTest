using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentExcelTemplateService : BaseEntity<int>
    {
        public int DepartmentExcelTemplateId { get; set; }
        [ForeignKey(nameof(DepartmentExcelTemplateId))]
        public DepartmentExcelTemplate DepartmentExcelTemplate { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))] public DepartmentService Service { get; set; }

        public string? AlternativeName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int OrderNumber { get; set; }
        public bool ShowChildService { get; set; } = false;
    }
}