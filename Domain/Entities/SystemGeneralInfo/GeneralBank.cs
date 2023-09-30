using Common.Entities;

namespace Domain.Entities.SystemGeneralInfo
{
    public class GeneralBank : BaseEntity<int>
    {
        public string Name { get; set; }
        public string BankCode { get; set; }
        public string PayerAccount { get; set; }
        public string ReciverBIC { get; set; }
    }
}