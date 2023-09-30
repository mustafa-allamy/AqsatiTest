using Common.Responses;
using Mediator;

namespace Application.CQRS.DepartmentInfo.DepartmentServices.Forms
{
    public class DeleteDepartmentServiceForm : ICommand<SuccessServiceResponse>
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
    }
}