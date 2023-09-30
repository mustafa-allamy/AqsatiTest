using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class GetDefaultExcelTemplateForm : BaseQuery, IRequest<SuccessServiceResponse<List<DefaultExcelTemplateColumnDto>>>
    {

    }
}