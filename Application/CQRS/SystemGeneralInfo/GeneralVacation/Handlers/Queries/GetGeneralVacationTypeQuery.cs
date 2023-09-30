using Application.CQRS.SystemGeneralInfo.GeneralVacation.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralVacation.Handlers.Queries
{
    public class GetGeneralVacationTypeQuery : IRequestHandler<GetGeneralVacationTypeForm, OneOf<SuccessServiceResponse<GeneralVacationTypeDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralVacationTypeQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<GeneralVacationTypeDto>, FailServiceResponse>> Handle(GetGeneralVacationTypeForm request, CancellationToken cancellationToken)
        {
            var generalVacationType = await _dbContext.GeneralVacationTypes
                .Where(x => x.Id == request.Id)
                .Include(x => x.VacationServiceRules).ThenInclude(x => x.Service)
                .Select(x => GeneralVacationTypeDto.FromEntity(x))
                .FirstOrDefaultAsync(cancellationToken);

            return generalVacationType is not null
                ? new SuccessServiceResponse<GeneralVacationTypeDto>().WithData(generalVacationType)
                : new FailServiceResponse();
        }
    }
}