using System;

namespace model.entities
{
    [Serializable]
    public class Entity<TId>
    {
        public TId Id { get; set; }

        public Entity(TId id)
        {
            Id = id;
        }
        public Entity()
        {
        }
    }
}