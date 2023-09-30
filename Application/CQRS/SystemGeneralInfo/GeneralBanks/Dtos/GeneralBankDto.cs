using Common.Dto;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos
{
    public class GeneralBankDto : BaseDto<GeneralBankDto, Domain.Entities.SystemGeneralInfo.GeneralBank>
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string BankCode { get; set; }
        public string PayerAccount { get; set; }
        public string ReciverBIC { get; set; }
    }

}
