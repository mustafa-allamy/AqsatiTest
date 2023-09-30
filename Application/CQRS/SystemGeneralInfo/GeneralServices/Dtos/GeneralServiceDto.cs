using Common.Dto;
using Domain.Entities.SystemGeneralInfo;
using Domain.Enums;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos
{
    public class GeneralServiceDto : BaseDto<GeneralServiceDto, GeneralService>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public ServiceKind? Kind { get; set; }
        public bool IsPercentage { get; set; }
        public ServiceType Type { get; set; }

        public int Priority { get; set; }

        public List<GeneralServiceDto> ChildServices { get; set; }

    }
}