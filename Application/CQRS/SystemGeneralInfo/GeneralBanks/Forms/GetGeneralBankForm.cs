using Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms
{
    public class GetGeneralBankForm : IRequest<OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}
