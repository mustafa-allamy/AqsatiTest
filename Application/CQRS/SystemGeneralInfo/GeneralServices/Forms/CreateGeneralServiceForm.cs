using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Domain.Enums;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Forms
{
    public class CreateGeneralServiceForm : BaseForm<CreateGeneralServiceForm, GeneralService>, ICommand<SuccessServiceResponse<GeneralServiceDto>>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int? ParentServiceId { get; set; }
        public ServiceKind Kind { get; set; }
        public bool IsPercentage { get; set; }
        public ServiceType Type { get; set; }

        public int Priority { get; set; }
    }
}