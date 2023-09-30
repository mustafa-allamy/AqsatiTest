using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class CreateDepartmentExcelTemplateForm : BaseForm<CreateDepartmentExcelTemplateForm, DepartmentExcelTemplate>, ICommand<SuccessServiceResponse<DepartmentExcelTemplateDto>>
    {

        public string Name { get; set; }
        public ICollection<AddDepartmentExcelTemplateColumnForm> Columns { get; set; }
        public ICollection<AddDepartmentExcelTemplateServiceForm> Services { get; set; }

        [JsonIgnore]
        public int? DepartmentId { get; set; }

    }
}