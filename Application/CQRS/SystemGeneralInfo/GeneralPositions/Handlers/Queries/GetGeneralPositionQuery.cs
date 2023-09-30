using Application.CQRS.SystemGeneralInfo.GeneralPositions.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralPositions.Forms;
using Common.Extensions;
using Common.Responses;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralPositions.Handlers.Queries
{
    public class GetGeneralPositionQuery : IRequestHandler<GetGeneralPositionForm, OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISharedCultureLocalizer _localizer;

        public GetGeneralPositionQuery(IApplicationDbContext dbContext, ISharedCultureLocalizer? localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralPositionDto>, FailServiceResponse>> Handle(GetGeneralPositionForm request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GeneralPositions
                .Where(x => x.Id == request.Id)
                .Include(x => x.GeneralService)
                .Select(x => GeneralPositionDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return entity != default
                ? new SuccessServiceResponse<GeneralPositionDto>().WithData(entity)
                : new FailServiceResponse().WithError(_localizer.GetLocalizedString("GeneralPosition.NotFound"));
        }
    }
}
