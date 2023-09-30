using Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms
{
    public class CreateGeneralBankForm : BaseForm<CreateGeneralBankForm, GeneralBank>,
        ICommand<OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>>

    {
        public string Name { get; set; }
        public string BankCode { get; set; }
        public string PayerAccount { get; set; }
        public string ReciverBIC { get; set; }
    }
}
