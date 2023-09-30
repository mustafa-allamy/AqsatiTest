using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralExcelTemplateColumns : BaseEntity<int>
    {

        public int GeneralExcelTemplateId { get; set; }
        [ForeignKey(nameof(GeneralExcelTemplateId))]
        public GeneralExcelTemplate GeneralExcelTemplate { get; set; }
        public int DefaultExcelTemplateColumnId { get; set; }
        [ForeignKey(nameof(DefaultExcelTemplateColumnId))]
        public DefaultExcelTemplateColumn DefaultExcelTemplateColumn { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;
        public int Order { get; set; }
    }
}