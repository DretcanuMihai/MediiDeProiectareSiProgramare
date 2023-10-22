using model.entities;

namespace model.validators.interfaces;

public interface IValidator<TId, TEntity> where TEntity : Entity<TId>
{
    
    /// <summary>
    /// validates an entity
    /// </summary>
    /// <param name="entity">said entity</param>
    /// <exception cref="ValidationException">if entity is invalid</exception>
    void Validate(TEntity entity);
}