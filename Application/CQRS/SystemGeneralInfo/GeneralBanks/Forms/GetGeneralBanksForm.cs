using Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms
{
    public class GetGeneralBanksForm : BaseQuery, IRequest<SuccessServiceResponse<List<GeneralBankDto>>>
    {
        public string? Name { get; set; }
        public string? BankCode { get; set; }
        public string? PayerAccount { get; set; }
        public string? ReciverBIC { get; set; }
    }
}
