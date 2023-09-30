using Common.Entities;

namespace Domain.Entities.SystemGeneralInfo
{
    public class DefaultExcelTemplateColumn : BaseEntity<int>
    {
        public string CulomnName { get; set; }
        public bool IsVisible { get; set; } = true;
        public int Order { get; set; }
    }
}