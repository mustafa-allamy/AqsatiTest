using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms
{
    public class GetAcademicAchievementForm : IRequest<OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}
