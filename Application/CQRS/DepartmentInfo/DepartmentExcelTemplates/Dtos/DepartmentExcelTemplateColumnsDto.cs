using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Dto;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos
{
    public class DepartmentExcelTemplateColumnsDto : BaseDto<DepartmentExcelTemplateColumnsDto, DepartmentExcelTemplateColumns>
    {


        public DefaultExcelTemplateColumnDto DefaultExcelTemplateColumn { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;
        public int Order { get; set; }
    }
}