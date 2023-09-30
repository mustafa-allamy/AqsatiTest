using Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms
{
    public class GetGeneralPositionForm : IRequest<OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}
