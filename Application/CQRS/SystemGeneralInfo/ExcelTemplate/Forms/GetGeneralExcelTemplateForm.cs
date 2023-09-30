using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class GetGeneralExcelTemplateForm : IRequest<OneOf<SuccessServiceResponse<GeneralExcelTemplateDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}