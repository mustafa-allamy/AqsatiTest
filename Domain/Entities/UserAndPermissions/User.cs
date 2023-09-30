using Common.Entities;
using Domain.Entities.Departments;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAndPermissions
{
    public class User : IdentityUser<int>, ISoftDeleteModel
    {

        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";


        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
        public ICollection<UserPermissionGroup> UserPermissionGroups { get; set; }


        public ICollection<UserUnit> UserUnits { get; set; }

        public int? DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        public bool IsDeleted { get; set; }
    }
}