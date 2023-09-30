using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms
{
    public class UpdateAcademicAchievementForm : BaseForm<UpdateAcademicAchievementForm, AcademicAchievement>,
        ICommand<OneOf<SuccessServiceResponse<AcademicAchievementDto>, FailServiceResponse>>
    {
        public string? Name { get; set; }
        public int? GeneralServiceId { get; set; }
    }
}
