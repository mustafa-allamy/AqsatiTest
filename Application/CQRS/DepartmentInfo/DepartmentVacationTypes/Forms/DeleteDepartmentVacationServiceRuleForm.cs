using Common.Responses;
using Mediator;

namespace Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms
{
    public class DeleteDepartmentVacationServiceRuleForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }

    }
}