using Common.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralService : BaseEntity<int>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int? ParentServiceId { get; set; }
        [ForeignKey(nameof(ParentServiceId))]
        public GeneralService? ParentService { get; set; }
        public ServiceKind? Kind { get; set; }
        public bool IsPercentage { get; set; }
        public ServiceType Type { get; set; }

        public int Priority { get; set; }



        public ICollection<GeneralService> ChildServices { get; set; }

    }
}