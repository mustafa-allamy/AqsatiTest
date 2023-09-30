using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SystemGeneralInfo
{
    public class AcademicAchievement : BaseEntity<int>
    {
        public string Name { get; set; }

        public int? GeneralServiceId { get; set; }
        [ForeignKey(nameof(GeneralServiceId))]
        public GeneralService? GeneralService { get; set; }
    }
}