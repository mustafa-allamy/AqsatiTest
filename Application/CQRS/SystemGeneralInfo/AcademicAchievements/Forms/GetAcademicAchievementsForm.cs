using Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Forms
{
    public class GetAcademicAchievementsForm : BaseQuery, IRequest<SuccessServiceResponse<List<AcademicAchievementDto>>>
    {
        public string? Name { get; set; }
        public int? GeneralServiceId { get; set; }
    }
}
