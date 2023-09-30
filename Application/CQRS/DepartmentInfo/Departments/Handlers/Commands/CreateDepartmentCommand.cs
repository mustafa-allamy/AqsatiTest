using Application.CQRS.DepartmentInfo.Departments.Dtos;
using Application.CQRS.DepartmentInfo.Departments.Forms;
using Common.Extensions;
using Common.Responses;
using Domain.Entities.Departments;
using Mediator;
using OneOf;
using Persistence;

namespace Application.CQRS.DepartmentInfo.Departments.Handlers.Commands
{
    public class CreateDepartmentCommand : ICommandHandler<CreateDepartmentForm, OneOf<SuccessServiceResponse<DepartmentDto>, FailServiceResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateDepartmentCommand(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<OneOf<SuccessServiceResponse<DepartmentDto>, FailServiceResponse>> Handle(CreateDepartmentForm command, CancellationToken cancellationToken)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            var department = command.ToEntity();

            try
            {
                await _dbContext.Departments.AddAsync(department, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                department.DepartmentReportSetting = await CreateDefaultDepartmentSettings(department.Id, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);

                return new FailServiceResponse().WithError("Department.Create.Fail");
            }
            return new SuccessServiceResponse<DepartmentDto>().WithData(DepartmentDto.FromEntity(department));
        }


        private async Task<DepartmentReportSetting> CreateDefaultDepartmentSettings(int departmentId, CancellationToken cancellationToken)
        {
            var settings = new DepartmentReportSetting()
            {
                ExcelCellsFontBold = false,
                ExcelCellsFontSize = 11,
                ExcelRowHigh = 20,
                ExcelRowsPerPage = 50,
                ExcelFooterMargin = 1.5M,
                ExcelHeaderMargin = .05M,
                ExcelLeftMargin = .05M,
                ExcelRightMargin = .05M,
                ExcelHasBorder = true,
                ExcelHeaderHigh = 60,
                ExcelHeaderFontBold = true,
                ExcelHeaderFontSize = 12,
                ExcelIsHeaderRotated = true,
                ExcelPrinterScale = 75,
                DepartmentId = departmentId,
                ExcelAutoFitColumns = true,
                ShowPageFooter = false,
                ShowPageTotal = true,
            };
            await _dbContext.DepartmentReportSettings.AddAsync(settings, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return settings;
        }
    }
}