using Application.CQRS.SystemGeneralInfo.Salaries.Dtos;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.Salaries.Forms
{
    public class GetSalariesForm : IRequest<SuccessServiceResponse<List<SalaryDto>>>
    {

    }
}