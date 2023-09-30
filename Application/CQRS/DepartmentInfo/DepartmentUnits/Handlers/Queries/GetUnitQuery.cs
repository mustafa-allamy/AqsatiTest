using Application.CQRS.DepartmentInfo.DepartmentUnits.Dtos;
using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.DepartmentInfo.DepartmentUnits.Handlers.Queries
{
    public class GetUnitQuery : IRequestHandler<GetUnitForm, OneOf<SuccessServiceResponse<UnitDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUnitQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<UnitDto>, FailServiceResponse>> Handle(GetUnitForm request, CancellationToken cancellationToken)
        {
            var unit = await _dbContext.Units
                .Where(x => x.Id == request.Id && x.DepartmentId == request.DepartmentId)
                .Select(x => UnitDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return unit is not null ? new SuccessServiceResponse<UnitDto>().WithData(unit) : new FailServiceResponse();
        }
    }
}