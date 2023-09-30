using Application.CQRS.DepartmentInfo.Departments.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.DepartmentInfo.Departments.Forms
{
    public class GetDepartmentsForm : BaseQuery, IRequest<SuccessServiceResponse<List<DepartmentDto>>>
    {
        public int? MinistryId { get; set; }

        public string? Name { get; set; }
        public string? Domain { get; set; }
    }
}