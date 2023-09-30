using Common.Entities;
using Domain.Entities.MinistryAndGov;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class Department : BaseEntity<int>
    {
        public int MinistryId { get; set; }
        [ForeignKey(nameof(MinistryId))] public Ministry Ministry { get; set; }


        public DepartmentSetting DepartmentSetting { get; set; }
        public DepartmentReportSetting DepartmentReportSetting { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

    }
}