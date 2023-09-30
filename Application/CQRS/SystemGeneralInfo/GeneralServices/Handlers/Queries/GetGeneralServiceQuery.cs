using Application.CQRS.SystemGeneralInfo.GeneralServices.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralServices.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralServices.Handlers.Queries
{
    public class GetGeneralServiceQuery : IRequestHandler<GetGeneralServiceForm, OneOf<SuccessServiceResponse<GeneralServiceDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralServiceQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<OneOf<SuccessServiceResponse<GeneralServiceDto>, FailServiceResponse>> Handle(GetGeneralServiceForm request, CancellationToken cancellationToken)
        {
            var generalService = await _dbContext.GeneralServices
                .Where(x => x.Id == request.Id && x.ParentServiceId == null)
                .Include(x => x.ChildServices)
                .Select(x => GeneralServiceDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return generalService is not null
                ? new SuccessServiceResponse<GeneralServiceDto>().WithData(generalService)
                : new FailServiceResponse();
        }
    }
}