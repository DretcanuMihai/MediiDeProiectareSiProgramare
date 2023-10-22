using System;
using model.entities;

namespace model.validators.interfaces;

public interface IFestivalValidator:IValidator<int,Festival>
{
    /// <summary>
    /// validates the id of a festival
    /// </summary>
    /// <param name="id">said id</param>
    /// <exception cref="ValidationException">if id is null</exception>
    void ValidateId(int id);
    
    /// <summary>
    /// validates a date
    /// </summary>
    /// <param name="date">said date</param>
    /// <exception cref="ValidationException">if date is null</exception>
    void ValidateDate(DateTime date);

}