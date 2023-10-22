using System.Collections.Generic;
using model.entities;

namespace persistence.interfaces
{
    public interface IRepository<TId, TEntity> where TEntity : Entity<TId>
    {
        void Add(TEntity festival);
        
        void Delete(TEntity festival);
        
        void Update(TEntity festival, TId id);

        TEntity FindById(TId id);

        IEnumerable<TEntity> FindAll();
        
        ICollection<TEntity> GetAll();
    }
}