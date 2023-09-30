using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralExcelTemplateService : BaseEntity<int>
    {
        public int GeneralExcelTemplateId { get; set; }
        [ForeignKey(nameof(GeneralExcelTemplateId))]
        public GeneralExcelTemplate GeneralExcelTemplate { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public GeneralService Service { get; set; }

        public string? AlternativeName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int OrderNumber { get; set; }
        public bool ShowChildService { get; set; } = false;
    }
}