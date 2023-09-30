using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms
{
    public class DeleteGeneralVacationServiceRuleForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }
    }
}