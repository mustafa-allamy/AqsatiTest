using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms
{
    public class DeleteGeneralJobTitleFrom : BaseForm<DeleteGeneralJobTitleFrom, GeneralJobTitle>, ICommand<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {

    }
}
