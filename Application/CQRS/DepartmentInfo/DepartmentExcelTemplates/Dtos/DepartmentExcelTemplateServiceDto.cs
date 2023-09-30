using Application.CQRS.DepartmentInfo.DepartmentServices.Dtos;
using Common.Dto;
using Domain.Entities.Departments;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos
{
    public class DepartmentExcelTemplateServiceDto : BaseDto<DepartmentExcelTemplateServiceDto, DepartmentExcelTemplateService>
    {

        public DepartmentServiceDto Service { get; set; }

        public string? AlternativeName { get; set; }
        public bool IsVisible { get; set; } = true;

        public int OrderNumber { get; set; }
        public bool ShowChildService { get; set; } = false;
    }
}