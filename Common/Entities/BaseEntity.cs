using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public abstract class BaseEntity<T> : ISoftDeleteModel
    {
        [Key]
        public T Id { get; init; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }


        public DateTime? UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }


        public bool IsDeleted { get; set; }


    }
}