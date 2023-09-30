using Common.Dto;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos
{
    public class GeneralJobTitleDto : BaseDto<GeneralJobTitleDto, Domain.Entities.SystemGeneralInfo.GeneralJobTitle>
    {
        public string Name { get; set; }

    }

}
