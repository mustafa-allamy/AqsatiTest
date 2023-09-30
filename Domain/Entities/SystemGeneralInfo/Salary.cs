using Common.Entities;

namespace Domain.Entities.SystemGeneralInfo
{
    public class Salary : BaseEntity<int>
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public int Level { get; set; }
        public List<Grade> Grades { get; set; }
    }
}