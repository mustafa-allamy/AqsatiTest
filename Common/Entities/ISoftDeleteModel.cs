namespace Common.Entities
{
    public interface ISoftDeleteModel
    {
        bool IsDeleted { get; set; }
    }
}