using Domain.Entities.Departments;
using Domain.Entities.MinistryAndGov;
using Domain.Entities.SystemGeneralInfo;
using Domain.Entities.UserAndPermissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistence
{
    public interface IApplicationDbContext
    {
        #region UserAndPermissions

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<UserPermissionGroup> UserPermissionGroups { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }

        #endregion

        #region MinistryAndGov

        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Ministry> Ministries { get; set; }

        #endregion

        #region Department

        public DbSet<Department> Departments { get; set; }
        //public DbSet<DepartmentBank> DepartmentBanks { get; set; }
        public DbSet<DepartmentExcelTemplate> DepartmentExcelTemplates { get; set; }
        public DbSet<DepartmentExcelTemplateColumns> DepartmentExcelTemplateColumns { get; set; }
        public DbSet<DepartmentExcelTemplateService> DepartmentExcelTemplateServices { get; set; }
        //public DbSet<DepartmentJobTitle> DepartmentJobTitles { get; set; }
        public DbSet<DepartmentReportSetting> DepartmentReportSettings { get; set; }
        public DbSet<DepartmentSetting> DepartmentSettings { get; set; }

        public DbSet<DepartmentService> DepartmentServices { get; set; }
        public DbSet<DepartmentVacationType> DepartmentVacationTypes { get; set; }
        public DbSet<DepartmentVacationServiceRule> DepartmentVacationServiceRules { get; set; }
        public DbSet<DepartmentReportUsersGroup> DepartmentReportUsersGroups { get; set; }
        public DbSet<DepartmentReportUser> DepartmentReportUsers { get; set; }
        public DbSet<Unit> Units { get; set; }

        #endregion



        #region SystemGeneralInfo

        public DbSet<GeneralBank> GeneralBanks { get; set; }
        public DbSet<AcademicAchievement> AcademicAchievements { get; set; }
        public DbSet<GeneralExcelTemplate> GeneralExcelTemplates { get; set; }
        public DbSet<GeneralExcelTemplateColumns> GeneralExcelTemplateColumns { get; set; }
        public DbSet<GeneralExcelTemplateService> GeneralExcelTemplateServices { get; set; }
        public DbSet<GeneralPosition> GeneralPositions { get; set; }
        public DbSet<GeneralJobTitle> GeneralJobTitles { get; set; }
        public DbSet<GeneralService> GeneralServices { get; set; }
        public DbSet<GeneralVacationType> GeneralVacationTypes { get; set; }
        public DbSet<GeneralVacationServiceRule> GeneralVacationServiceRules { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        public DbSet<DefaultExcelTemplateColumn> DefaultExcelTemplateColumns { get; set; }
        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        DatabaseFacade Database { get; }

    }
}