using Application.CQRS.DepartmentInfo.Departments.Dtos;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.DepartmentInfo.Departments.Forms
{
    public class GetDepartmentForm : IRequest<OneOf<SuccessServiceResponse<DepartmentDto>, FailServiceResponse>>
    {
        public int Id { get; set; }
    }
}