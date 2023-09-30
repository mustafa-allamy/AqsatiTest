using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms
{
    public class GetGeneralJobTitleForm : IRequest<OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}
