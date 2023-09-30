using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class DeleteDepartmentExcelTemplateForm : ICommand<SuccessServiceResponse>
    {
        [JsonIgnore]
        public int? DepartmentId { get; set; }

        public int Id { get; set; }
    }
}