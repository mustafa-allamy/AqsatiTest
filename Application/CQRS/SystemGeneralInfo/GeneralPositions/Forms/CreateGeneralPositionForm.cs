using Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms
{
    public class CreateGeneralPositionForm : BaseForm<CreateGeneralPositionForm, GeneralPosition>,
        ICommand<OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>>

    {
        public string Name { get; set; }
        public int? GeneralServiceId { get; set; }

    }
}
