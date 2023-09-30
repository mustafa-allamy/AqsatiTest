using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class GetGeneralExcelTemplatesForm : BaseQuery, IRequest<SuccessServiceResponse<List<GeneralExcelTemplateDto>>>
    {
        public string? Name { get; set; }
    }
}