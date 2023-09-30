using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.User.Forms
{
    public class GetUserUnitsForm : BaseQuery, IRequest<SuccessServiceResponse<List<UnitDto>>>
    {
        [JsonIgnore] public int UserId { get; set; }
    }
}