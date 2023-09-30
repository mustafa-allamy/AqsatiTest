using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Common.Responses;
using Mediator;
using System.Text.Json.Serialization;

namespace Application.CQRS.User.Forms
{
    public class AddRemoveUserUnitsForm : ICommand<SuccessServiceResponse<List<UnitDto>>>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public List<int> UnitsIds { get; set; }
        [JsonIgnore]
        public int? DepartmentId { get; set; }
    }
}