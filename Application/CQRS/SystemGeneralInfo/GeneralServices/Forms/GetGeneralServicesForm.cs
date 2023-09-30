using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Enums;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Forms
{
    public class GetGeneralServicesForm : BaseQuery, IRequest<SuccessServiceResponse<List<GeneralServiceDto>>>
    {
        public string? Name { get; set; }
        public ServiceType Type { get; set; }
        public ServiceKind? Kind { get; set; }
        public bool? IsPercentage { get; set; }

    }
}