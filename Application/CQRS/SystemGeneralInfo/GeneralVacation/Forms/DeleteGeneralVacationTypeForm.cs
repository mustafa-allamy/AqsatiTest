using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms
{
    public class DeleteGeneralVacationTypeForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }

    }
}