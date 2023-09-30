using Common.Entities;
using Domain.Entities.Departments;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAndPermissions
{
    public class UserUnit : BaseEntity<int>
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))] public User User { get; set; }

        public int UnitId { get; set; }
        [ForeignKey(nameof(UnitId))] public Unit Unit { get; set; }


    }
}