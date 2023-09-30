using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Common.Dto;
using Domain.Entities.SystemGeneralInfo;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos
{
    public class GeneralExcelTemplateServiceDto : BaseDto<GeneralExcelTemplateServiceDto, GeneralExcelTemplateService>
    {
        public GeneralServiceDto Service { get; set; }

        public string? AlternativeName { get; set; }

        public int OrderNumber { get; set; }
        public bool ShowChildService { get; set; }
    }
}