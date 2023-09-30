using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms
{
    public class DeleteGeneralExcelTemplateForm : ICommand<SuccessServiceResponse<bool>>
    {
        public int Id { get; set; }
    }
}