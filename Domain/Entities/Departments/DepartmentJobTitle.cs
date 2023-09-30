using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentJobTitle : BaseEntity<int>
    {
        public int? GeneralJobTitleId { get; set; }
        [ForeignKey(nameof(GeneralJobTitleId))]
        public GeneralJobTitle? GeneralJobTitle { get; set; }

        public string Name { get; set; }


        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}