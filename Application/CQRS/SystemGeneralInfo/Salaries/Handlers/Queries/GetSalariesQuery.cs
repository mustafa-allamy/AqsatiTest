using Application.CQRS.SystemGeneralInfo.Salaries.Dtos;
using Application.CQRS.SystemGeneralInfo.Salaries.Forms;
using Common.Extensions;
using Common.Responses;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CQRS.SystemGeneralInfo.Salaries.Handlers.Queries
{
    public class GetSalariesQuery : IRequestHandler<GetSalariesForm, SuccessServiceResponse<List<SalaryDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetSalariesQuery(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<SuccessServiceResponse<List<SalaryDto>>> Handle(GetSalariesForm request, CancellationToken cancellationToken)
        {
            var salaries = await _dbContext.Salaries
                .Include(x => x.Grades)
                .Select(x => SalaryDto.FromEntity(x))
                .ToListAsync(cancellationToken);

            return new SuccessServiceResponse<List<SalaryDto>>().WithData(salaries).WithCount(salaries.Count);
        }
    }
}