using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentExcelTemplate : BaseEntity<int>
    {
        public int? GeneralExcelTemplateId { get; set; }
        [ForeignKey(nameof(GeneralExcelTemplateId))]
        public GeneralExcelTemplate? GeneralExcelTemplate { get; set; }

        public string Name { get; set; }
        public ICollection<DepartmentExcelTemplateColumns> Columns { get; set; }
        public ICollection<DepartmentExcelTemplateService> Services { get; set; }


        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}