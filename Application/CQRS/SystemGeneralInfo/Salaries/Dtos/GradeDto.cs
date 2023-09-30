using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.Salaries.Dtos
{
    public class GradeDto : BaseDto<GradeDto, Grade>
    {
        public int GradeNumber { get; set; }
        public int Amount { get; set; }
    }
}