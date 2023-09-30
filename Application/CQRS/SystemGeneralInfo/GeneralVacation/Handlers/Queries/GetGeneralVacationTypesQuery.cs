using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Queries
{
    public class GetGeneralVacationTypesQuery : IRequestHandler<GetGeneralVacationTypesForm, SuccessServiceResponse<List<GeneralVacationTypeDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralVacationTypesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<GeneralVacationTypeDto>>> Handle(GetGeneralVacationTypesForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GeneralVacationTypes
                .WhereIf(request.Name is not null, x => x.Name.Contains(request.Name))
                .Select(x => GeneralVacationTypeDto.FromEntity(x));

            return new SuccessServiceResponse<List<GeneralVacationTypeDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}