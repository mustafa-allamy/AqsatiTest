using Common.Dto;

namespace Application.CQRS.SystemGeneralInfo.AcademicAchievements.Dtos
{
    public class AcademicAchievementDto : BaseDto<AcademicAchievementDto, Domain.Entities.SystemGeneralInfo.AcademicAchievement>
    {
        public string Name { get; set; }
        public int? GeneralServiceId { get; set; }

    }

}
