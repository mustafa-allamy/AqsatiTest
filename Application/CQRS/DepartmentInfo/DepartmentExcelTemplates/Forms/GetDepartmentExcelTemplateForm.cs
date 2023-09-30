using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Dtos;
using Common.Responses;
using Mediator;
using OneOf;
using System.Text.Json.Serialization;

namespace Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms
{
    public class GetDepartmentExcelTemplateForm : IRequest<OneOf<SuccessServiceResponse<DepartmentExcelTemplateDto>, FailServiceResponse>>
    {
        [JsonIgnore]
        public int? DepartmentId { get; set; }

        public int Id { get; set; }
    }
}