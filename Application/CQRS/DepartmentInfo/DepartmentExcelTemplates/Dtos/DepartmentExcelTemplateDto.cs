using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Dto;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos
{
    public class DepartmentExcelTemplateDto : BaseDto<DepartmentExcelTemplateDto, DepartmentExcelTemplate>
    {
        public GeneralExcelTemplateDto GeneralExcelTemplate { get; set; }

        public string Name { get; set; }
        public List<DepartmentExcelTemplateColumnsDto> Columns { get; set; }
        public List<DepartmentExcelTemplateServiceDto> Services { get; set; }
    }
}