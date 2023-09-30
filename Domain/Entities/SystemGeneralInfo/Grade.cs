using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SystemGeneralInfo
{
    public class Grade : BaseEntity<int>
    {
        public int SalaryId { get; set; }
        [ForeignKey(nameof(SalaryId))]
        public Salary Salary { get; set; }
        public int GradeNumber { get; set; }
        public int Amount { get; set; }
    }
}