using Common.Entities;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralExcelTemplate : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<GeneralExcelTemplateColumns> Columns { get; set; }
        public ICollection<GeneralExcelTemplateService> Services { get; set; }
    }
}