namespace Infrastructure.Authentication
{
    public static class PermissionsConst
    {
        #region Users

        public const string RegisterUser = "User.Register";
        public const string GetUsers = "User.GetList";
        public const string GetUser = "User.GetById";
        public const string UpdateUser = "User.Update";
        public const string UpdateUserPermissions = "User.UpdatePermissions";
        public const string UpdateUserUnits = "User.UpdateUnits";
        public const string GetUserUnits = "User.GetUnits";
        public const string AddUserPermissionsGroup = "User.AddUserPermissionsGroup";
        public const string RemoveUserPermissionsGroup = "User.RemoveUserPermissionsGroup";
        #endregion




        #region Permission

        public const string UpdatePermission = "Permission.Update";
        public const string GetPermissions = "Permission.GetList";

        #endregion

        #region PermissionGroup
        public const string GetPermissionGroup = "PermissionGroup.GetById";
        public const string GetPermissionGroups = "PermissionGroup.GetList";
        public const string CreatePermissionGroup = "PermissionGroup.Create";
        public const string UpdatePermissionGroup = "PermissionGroup.Update";
        public const string DeletePermissionGroup = "PermissionGroup.Delete";
        public const string UpdateGroupPermissions = "PermissionGroup.UpdatePermissions";


        #endregion

        #region Governorate
        public const string GetGovernorate = "Governorate.GetById";
        public const string GetGovernorates = "Governorate.GetList";
        public const string CreateGovernorate = "Governorate.Create";
        public const string UpdateGovernorate = "Governorate.Update";
        public const string DeleteGovernorate = "Governorate.Delete";


        #endregion

        #region GeneralJobTitle
        public const string GetGeneralJobTitle = "GeneralJobTitle.GetById";
        public const string GetGeneralJobTitles = "GeneralJobTitle.GetList";
        public const string CreateGeneralJobTitle = "GeneralJobTitle.Create";
        public const string UpdateGeneralJobTitle = "GeneralJobTitle.Update";
        public const string DeleteGeneralJobTitle = "GeneralJobTitle.Delete";
        #endregion

        #region GeneralPosition
        public const string GetGeneralPosition = "GeneralPosition.GetById";
        public const string GetGeneralPositions = "GeneralPosition.GetList";
        public const string CreateGeneralPosition = "GeneralPosition.Create";
        public const string UpdateGeneralPosition = "GeneralPosition.Update";
        public const string DeleteGeneralPosition = "GeneralPosition.Delete";

        #endregion

        #region AcademicAchievement
        public const string GetAcademicAchievement = "AcademicAchievement.GetById";
        public const string GetAcademicAchievements = "AcademicAchievement.GetList";
        public const string CreateAcademicAchievement = "AcademicAchievement.Create";
        public const string UpdateAcademicAchievement = "AcademicAchievement.Update";
        public const string DeleteAcademicAchievement = "AcademicAchievement.Delete";

        #endregion

        #region GeneralBank
        public const string GetGeneralBank = "GeneralBank.GetById";
        public const string GetGeneralBanks = "GeneralBank.GetList";
        public const string CreateGeneralBank = "GeneralBank.Create";
        public const string UpdateGeneralBank = "GeneralBank.Update";
        public const string DeleteGeneralBank = "GeneralBank.Delete";


        #endregion


        #region GeneralExcelTemplate
        public const string GetGeneralExcelTemplate = "GeneralExcelTemplate.GetById";
        public const string GetGeneralExcelTemplates = "GeneralExcelTemplate.GetList";
        public const string CreateGeneralExcelTemplate = "GeneralExcelTemplate.Create";
        public const string UpdateGeneralExcelTemplate = "GeneralExcelTemplate.Update";
        public const string DeleteGeneralExcelTemplate = "GeneralExcelTemplate.Delete";
        public const string AddGeneralExcelTemplateServices = "GeneralExcelTemplate.AddServices";



        #endregion
        #region DefaultExcelTemplate

        public const string GetDefaultExcelTemplateColumns = "DefaultExcelTemplate.GetColumns";



        #endregion

        #region GeneralServices

        public const string GetGeneralService = "GeneralService.GetById";
        public const string GetGeneralServices = "GeneralService.GetList";
        public const string CreateGeneralService = "GeneralService.Create";
        public const string UpdateGeneralService = "GeneralService.Update";
        public const string DeleteGeneralService = "GeneralService.Delete";

        #endregion

        #region GeneralVacationType

        public const string GetGeneralVacationType = "GeneralVacationType.GetById";
        public const string GetGeneralVacationTypes = "GeneralVacationType.GetList";
        public const string CreateGeneralVacationType = "GeneralVacationType.Create";
        public const string UpdateGeneralVacationType = "GeneralVacationType.Update";
        public const string DeleteGeneralVacationType = "GeneralVacationType.Delete";
        public const string AddGeneralVacationTypeRule = "GeneralVacationType.AddRule";
        public const string DeleteGeneralVacationTypeRule = "GeneralVacationType.DeleteRule";

        #endregion

        #region Department
        public const string GetDepartment = "Department.GetById";
        public const string GetDepartments = "Department.GetList";
        public const string CreateDepartment = "Department.Create";
        public const string GetDepartmentUploadTemplate = "Department.GetEmployeeUploadTemplate";
        public const string UploadEmployeesFile = "Department.UploadEmployeesFile";
        public const string AddGeneralExcelTemplateToDepartment = "Department.AddGeneralExcelTemplates";
        public const string AddGeneralServicesToDepartment = "Department.AddGeneralServices";
        public const string AddGeneralVacationTypesToDepartment = "Department.AddGeneralVacationTypes";


        #endregion

        #region DepartmentExcelTemplate
        public const string GetDepartmentExcelTemplate = "DepartmentExcelTemplate.GetById";
        public const string GetDepartmentExcelTemplates = "DepartmentExcelTemplate.GetList";
        public const string CreateDepartmentExcelTemplate = "DepartmentExcelTemplate.Create";
        public const string UpdateDepartmentExcelTemplate = "DepartmentExcelTemplate.Update";
        public const string DeleteDepartmentExcelTemplate = "DepartmentExcelTemplate.Delete";
        public const string AddDepartmentExcelTemplateServices = "DepartmentExcelTemplate.AddServices";

        #endregion
        #region DepartmentServices

        public const string GetDepartmentService = "DepartmentService.GetById";
        public const string GetDepartmentServices = "DepartmentService.GetList";
        public const string CreateDepartmentService = "DepartmentService.Create";
        public const string UpdateDepartmentService = "DepartmentService.Update";
        public const string DeleteDepartmentService = "DepartmentService.Delete";

        #endregion

        #region DepartmentVacationType

        public const string GetDepartmentVacationType = "DepartmentVacationType.GetById";
        public const string GetDepartmentVacationTypes = "DepartmentVacationType.GetList";
        public const string CreateDepartmentVacationType = "DepartmentVacationType.Create";
        public const string UpdateDepartmentVacationType = "DepartmentVacationType.Update";
        public const string DeleteDepartmentVacationType = "DepartmentVacationType.Delete";
        public const string AddDepartmentVacationTypeRule = "DepartmentVacationType.AddRule";
        public const string DeleteDepartmentVacationTypeRule = "DepartmentVacationType.DeleteRule";

        #endregion

        #region Unit
        public const string GetUnit = "Unit.GetById";
        public const string GetUnits = "Unit.GetList";
        public const string CreateUnit = "Unit.Create";
        public const string UpdateUnit = "Unit.Update";
        public const string DeleteUnit = "Unit.Delete";

        #endregion

        #region Salary

        public const string GetSalaries = "Salary.GetList";

        #endregion


        #region Employee
        public const string GetEmployeeEnrollments = "Employee.GetEnrollments";
        public const string GetEmployeeSalary = "Employee.GetSalary";
        public const string AddEmployeeEnrollment = "Employee.AddEnrollments";
        public const string DisableEmployeeEnrollment = "Employee.DisableEnrollments";
        public const string AddEmployeeService = "Employee.AddService";
        public const string DeleteEmployeeService = "Employee.DeleteService";
        public const string AddBasicSalaryAddition = "Employee.AddBasicSalaryAddition";
        public const string DeleteBasicSalaryAddition = "Employee.DeleteBasicSalaryAddition";


        #endregion

        #region Payment

        public const string CalculatePayments = "Payment.Calculate";


        #endregion
    }
}
