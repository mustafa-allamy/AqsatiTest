using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms
{
    public class DeleteAcademicAchievementFrom : BaseForm<DeleteAcademicAchievementFrom, AcademicAchievement>, ICommand<OneOf<SuccessServiceResponse<bool>, FailServiceResponse>>
    {

    }
}
