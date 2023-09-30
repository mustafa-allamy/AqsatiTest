using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentService : BaseEntity<int>
    {
        public int? GeneralServiceId { get; set; }
        [ForeignKey(nameof(GeneralServiceId))]
        public GeneralService? GeneralService { get; set; }

        public string Name { get; set; }
        public double Amount { get; set; }
        public int? ParentServiceId { get; set; }
        [ForeignKey(nameof(ParentServiceId))]
        public DepartmentService? ParentService { get; set; }
        public ServiceKind Kind { get; set; }
        public bool IsPercentage { get; set; }
        public ServiceType Type { get; set; }
        public int Priority { get; set; }

        public ICollection<DepartmentService> ChildServices { get; set; }



        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}