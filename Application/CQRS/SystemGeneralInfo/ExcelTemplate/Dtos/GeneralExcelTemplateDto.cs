using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos
{
    public class GeneralExcelTemplateDto : BaseDto<GeneralExcelTemplateDto, GeneralExcelTemplate>
    {
        public string Name { get; set; }
        public List<GeneralExcelTemplateColumnDto> Columns { get; set; }
        public List<GeneralExcelTemplateServiceDto> Services { get; set; }
    }
}