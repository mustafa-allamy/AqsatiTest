using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms
{
    public class GetGeneralJobTitlesForm : BaseQuery, IRequest<SuccessServiceResponse<List<GeneralJobTitleDto>>>
    {
        public string? Name { get; set; }
    }
}
