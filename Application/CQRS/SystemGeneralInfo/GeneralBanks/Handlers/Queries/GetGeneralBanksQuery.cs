using Application.CQRS.SystemGeneralInfo.GeneralBanks.Dtos;
using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.GeneralBanks.Handlers.Queries
{
    public class GetGeneralBanksQuery : IRequestHandler<GetGeneralBanksForm, SuccessServiceResponse<List<GeneralBankDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetGeneralBanksQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<GeneralBankDto>>> Handle(GetGeneralBanksForm request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GeneralBanks
                .WhereIf(request.Name != null, x => x.Name.Contains(request.Name))
                .WhereIf(request.ReciverBIC != null, x => x.ReciverBIC.Contains(request.ReciverBIC))
                .WhereIf(request.BankCode != null, x => x.BankCode.Contains(request.BankCode))
                .WhereIf(request.PayerAccount != null, x => x.PayerAccount.Contains(request.PayerAccount))
                .Select(x => GeneralBankDto.FromEntity(x));

            return new SuccessServiceResponse<List<GeneralBankDto>>()
                .WithData(await query.ApplyPagination(request).ToListAsync(cancellationToken))
                .WithCount(await query.CountAsync(cancellationToken));

        }
    }
}
