using Common.Dto;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos
{
    public class GeneralPositionDto : BaseDto<GeneralPositionDto, Domain.Entities.SystemGeneralInfo.GeneralPosition>
    {
        public string Name { get; set; }
        public int? GeneralServiceId { get; set; }

    }

}
