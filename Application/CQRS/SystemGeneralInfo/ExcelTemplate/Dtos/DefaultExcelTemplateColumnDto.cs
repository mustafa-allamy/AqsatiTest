using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos
{
    public class DefaultExcelTemplateColumnDto : BaseDto<DefaultExcelTemplateColumnDto, DefaultExcelTemplateColumn>
    {
        public string CulomnName { get; set; }
        public bool IsVisible { get; set; } = true;
        public int Order { get; set; }
    }
}