using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms
{
    public class GetGeneralVacationTypeForm : IRequest<OneOf<SuccessServiceResponse<GeneralVacationTypeDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}