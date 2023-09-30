using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAndPermissions
{
    public class Permission : BaseEntity<int>
    {
        [NotMapped]
        public string Name => ClassName + "_" + MethodName + "_" + MethodType;
        public string PolicyName { get; set; }
        public string UserDefinedName { get; set; }

        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string MethodType { get; set; }
        public ICollection<UserPermission> UsersPermissions { get; set; }


    }
}