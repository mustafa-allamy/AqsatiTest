using Common.Entities;

namespace Domain.Entities.MinistryAndGov
{
    public class Ministry : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}