using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms
{
    public class GetGeneralVacationTypesForm : BaseQuery, IRequest<SuccessServiceResponse<List<GeneralVacationTypeDto>>>
    {
        public string? Name { get; set; }

    }
}