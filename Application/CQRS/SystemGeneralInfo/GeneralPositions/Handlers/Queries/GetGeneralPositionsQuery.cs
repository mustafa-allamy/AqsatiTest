using Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Handlers.Queries
{
    public class GetGeneralPositionsQuery : IRequestHandler<GetGeneralPositionsForm, SuccessServiceResponse<List<GeneralPositionDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralPositionsQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<GeneralPositionDto>>> Handle(GetGeneralPositionsForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GeneralPositions
                .WhereIf(request.Name != null, x => x.Name.Contains(request.Name))
                .WhereIf(request.GeneralServiceId != null, x => x.GeneralServiceId == request.GeneralServiceId)

                .Select(x => GeneralPositionDto.FromEntity(x));

            return new SuccessServiceResponse<List<GeneralPositionDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}
