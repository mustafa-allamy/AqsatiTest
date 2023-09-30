using Common.Entities;
using Domain.Entities.SystemGeneralInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Departments
{
    public class DepartmentPosition : BaseEntity<int>
    {
        public string Name { get; set; }

        public int? GeneralPositionId { get; set; }
        [ForeignKey(nameof(GeneralPositionId))]
        public GeneralPosition? GeneralPosition { get; set; }


        public int? GeneralServiceId { get; set; }
        [ForeignKey(nameof(GeneralServiceId))]
        public GeneralService? GeneralService { get; set; }
    }
}