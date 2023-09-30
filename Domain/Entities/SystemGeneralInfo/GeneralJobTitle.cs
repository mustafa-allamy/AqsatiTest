using Common.Entities;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralJobTitle : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}