using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Handlers.Queries
{
    public class GetGeneralServicesQuery : IRequestHandler<GetGeneralServicesForm, SuccessServiceResponse<List<GeneralServiceDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralServicesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<SuccessServiceResponse<List<GeneralServiceDto>>> Handle(GetGeneralServicesForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GeneralServices
                .Where(x => x.Type == request.Type && x.ParentServiceId == null)
                .WhereIf(request.Name is not null, x => x.Name.Contains(request.Name))
                .WhereIf(request.IsPercentage is not null, x => x.IsPercentage == request.IsPercentage)
                .WhereIf(request.Kind is not null, x => x.Kind == request.Kind)
                .Select(x => GeneralServiceDto.FromEntity(x));

            return new SuccessServiceResponse<List<GeneralServiceDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));
        }
    }
}