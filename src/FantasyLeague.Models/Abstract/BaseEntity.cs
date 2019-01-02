using System;

namespace FantasyLeague.Models.Abstract
{
    public abstract class BaseEntity : IComparable<BaseEntity>
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int CompareTo(BaseEntity other)
        {
            if (other == null)
            {
                return 1;
            }
            return Id.CompareTo(other.Id);
        }
    }
}
