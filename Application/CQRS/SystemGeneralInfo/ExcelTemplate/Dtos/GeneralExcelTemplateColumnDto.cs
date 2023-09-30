using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos
{
    public class GeneralExcelTemplateColumnDto : BaseDto<GeneralExcelTemplateColumnDto, GeneralExcelTemplateColumns>
    {

        public DefaultExcelTemplateColumnDto DefaultExcelTemplateColumn { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;
        public int Order { get; set; }
    }
}