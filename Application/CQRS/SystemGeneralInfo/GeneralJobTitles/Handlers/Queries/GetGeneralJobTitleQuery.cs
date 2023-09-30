using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Forms;
using Common.Extensions;
using Common.Responses;
using LazZiya.ExpressLocalization;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralJobTitles.Handlers.Queries
{
    public class GetGeneralJobTitleQuery : IRequestHandler<GetGeneralJobTitleForm, OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISharedCultureLocalizer _localizer;

        public GetGeneralJobTitleQuery(IApplicationDbContext dbContext, ISharedCultureLocalizer? localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralJobTitleDto>, FailServiceResponse>> Handle(GetGeneralJobTitleForm request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GeneralJobTitles
                .Where(x => x.Id == request.Id)
                .Select(x => GeneralJobTitleDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return entity != default
                ? new SuccessServiceResponse<GeneralJobTitleDto>().WithData(entity)
                : new FailServiceResponse().WithError(_localizer.GetLocalizedString("GeneralJobTitle.NotFound"));
        }
    }
}
