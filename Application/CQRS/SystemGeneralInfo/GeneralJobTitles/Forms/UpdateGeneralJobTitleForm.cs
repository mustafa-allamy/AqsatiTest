using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using OneOf;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms
{
    public class UpdateGeneralJobTitleForm : BaseForm<UpdateGeneralJobTitleForm, GeneralJobTitle>,
        ICommand<OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>>
    {
        public string Name { get; set; }
    }
}
