using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms
{
    public class DeleteGeneralPositionFrom : BaseForm<DeleteGeneralPositionFrom, GeneralPosition>, ICommand<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {

    }
}
