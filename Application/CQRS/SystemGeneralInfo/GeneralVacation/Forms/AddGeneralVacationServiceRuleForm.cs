using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms
{
    public class AddGeneralVacationServiceRuleForm : BaseForm<AddGeneralVacationServiceRuleForm, GeneralVacationServiceRule>, ICommand<SuccessServiceResponse<GeneralVacationServiceRuleDto>>
    {
        [JsonIgnore]
        public int VacationTypeId { get; set; }

        public int ServiceId { get; set; }
        public bool NotEffectedByBasicSalaryDeduction { get; set; }
        public int Amount { get; set; }

    }
}