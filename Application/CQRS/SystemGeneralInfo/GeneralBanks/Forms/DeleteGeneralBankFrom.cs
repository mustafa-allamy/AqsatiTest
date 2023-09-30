using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms
{
    public class DeleteGeneralBankFrom : BaseForm<DeleteGeneralBankFrom, GeneralBank>, ICommand<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {

    }
}
