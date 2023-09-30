using Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using Common.Extensions;
using Common.Responses;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Handlers.Queries
{
    public class GetGeneralBankQuery : IRequestHandler<GetGeneralBankForm, OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISharedCultureLocalizer _localizer;

        public GetGeneralBankQuery(IApplicationDbContext dbContext, ISharedCultureLocalizer? localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralBankDto>, FailServiceResponse>> Handle(GetGeneralBankForm request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GeneralBanks
                .Where(x => x.Id == request.Id)
                .Select(x => GeneralBankDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return entity != default
                ? new SuccessServiceResponse<GeneralBankDto>().WithData(entity)
                : new FailServiceResponse().WithError(_localizer.GetLocalizedString("GeneralBank.NotFound"));
        }
    }
}
