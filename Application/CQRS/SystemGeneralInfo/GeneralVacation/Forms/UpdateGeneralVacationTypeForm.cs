using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Common.Forms;
using Common.Responses;
using Domain.Entities.SystemGeneralInfo;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms
{
    public class UpdateGeneralVacationTypeForm : BaseForm<UpdateGeneralVacationTypeForm, GeneralVacationType>, ICommand<SuccessServiceResponse<GeneralVacationTypeDto>>
    {
        public string? Name { get; set; }
        public int? Amount { get; set; }
        public bool? EffectedByAllAllowances { get; set; }
        public bool? EffectedByAllDeductions { get; set; }
    }
}