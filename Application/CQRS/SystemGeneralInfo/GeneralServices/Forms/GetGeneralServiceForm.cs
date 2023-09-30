using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Forms
{
    public class GetGeneralServiceForm : IRequest<OneOf<SuccessServiceResponse<GeneralServiceDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}