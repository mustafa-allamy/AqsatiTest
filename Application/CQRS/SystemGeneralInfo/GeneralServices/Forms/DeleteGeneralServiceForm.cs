using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Forms
{
    public class DeleteGeneralServiceForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }
    }
}