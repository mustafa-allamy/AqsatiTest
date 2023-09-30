using Domain.Entities.SystemGeneralInfo;
using Domain.Entities.UserAndPermissions;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Reflection;

namespace WebAPi
{
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Permissions

        public void SeedPermissions(Assembly assembly)
        {
            var seededPermissions = _dbContext.Permissions.ToList();
            var newPermissions = GetControllersPermissions(assembly);

            var permissionsToUpdate = seededPermissions.Where(x => newPermissions.Select(y => y.Name).Contains(x.Name)).ToList();
            foreach (var permission in permissionsToUpdate)
            {
                var updatedPermission = newPermissions.First(x => x.Name == permission.Name);
                permission.PolicyName = updatedPermission.PolicyName;
                permission.UserDefinedName = updatedPermission.GivenName;
                _dbContext.Permissions.Update(permission);
            }

            var permissionsToSeed = newPermissions.Where(x => !seededPermissions.Select(y => y.Name).Contains(x.Name))
                .Select(x => new Permission()
                {
                    ClassName = x.ClassName,
                    MethodType = x.MethodType,
                    MethodName = x.MethodName,
                    PolicyName = x.PolicyName,
                    UserDefinedName = x.GivenName,
                });

            _dbContext.Permissions.AddRange(permissionsToSeed);
            _dbContext.SaveChanges();
        }

        private List<ControllerPermissionDto> GetControllersPermissions(Assembly assembly)
        {
            return assembly.GetTypes().SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(HasPermissionAttribute), false).Length > 0)
                .Select(methodInfo => new ControllerPermissionDto()
                {
                    PolicyName = methodInfo.GetCustomAttribute<HasPermissionAttribute>()!.Permission,
                    GivenName = methodInfo.GetCustomAttribute<HasPermissionAttribute>()!.GivenName,
                    ClassName = methodInfo.DeclaringType!.Name,
                    MethodName = methodInfo.Name,
                    MethodType = methodInfo.GetCustomAttribute<HttpMethodAttribute>()!.HttpMethods.First(),
                }).ToList();
        }


        #endregion


        public void SeedAdmin()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email.Equals("admin@mail.com"));
            if (user == null)
            {
                user = new User()
                {
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    UserName = "admin@mail.com",
                    NormalizedUserName = "ADMIN@MAIL.COM",
                    FirstName = "admin",
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEMeClSdFEDBUPaZPbDHCcXQwiApN8ZSpfSAbaKLDmYqA9a/4a2AoYgmJMw3NQUzk8w==",
                    SecurityStamp = "BMEQQTL22E5MQRELUO4WSH4UNBVVONSC",
                    ConcurrencyStamp = "5fff4261-ad5f-4443-8465-cd5fd9a0cab2",
                    PhoneNumber = "07822180000",

                };
                _dbContext.Users.Add(user);
            }

            _dbContext.SaveChanges();
            var userPermissions = _dbContext.UserPermissions.Where(x => x.UserId == user.Id).Select(x => x.PermissionId).ToList();

            var permissions = _dbContext.Permissions.Where(x => !userPermissions.Contains(x.Id)).Select(x => new UserPermission()
            {
                PermissionId = x.Id,
                UserId = user.Id
            }).ToList();
            _dbContext.UserPermissions.AddRange(permissions);
            _dbContext.SaveChanges();

        }

        public void SeedDefaultExcelTemplateColumns()
        {
            var columns = _dbContext.DefaultExcelTemplateColumns.Select(x => x.CulomnName).ToList();
            if (!columns.Contains("الاسم"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "الاسم",
                    Order = 1
                });

            if (!columns.Contains("العنوان الوظيفي"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "العنوان الوظيفي",
                    Order = 2
                });

            if (!columns.Contains("الدرجة الوظيفية"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "الدرجة الوظيفية",
                    Order = 3
                });

            if (!columns.Contains("المرحلة"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "المرحلة",
                    Order = 4
                });

            if (!columns.Contains("الراتب الاسمي"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "الراتب الاسمي",
                    Order = 5
                });
            if (!columns.Contains("الاضافات على الراتب"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "الاضافات على الراتب",
                    Order = 6
                });
            if (!columns.Contains("الاسمي والمتغير عليه"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "الاسمي والمتغير عليه",
                    Order = 7
                });
            if (!columns.Contains("القسم"))
                _dbContext.DefaultExcelTemplateColumns.Add(new DefaultExcelTemplateColumn()
                {
                    CulomnName = "القسم",
                    Order = 8
                });

            _dbContext.SaveChanges();

        }
    }
}