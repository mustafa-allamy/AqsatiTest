using Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms
{
    public class GetGeneralPositionsForm : BaseQuery, IRequest<SuccessServiceResponse<List<GeneralPositionDto>>>
    {
        public string? Name { get; set; }
        public int? GeneralServiceId { get; set; }
    }
}
