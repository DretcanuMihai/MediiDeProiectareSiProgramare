using System;
using System.Collections.Generic;
using model.entities;

namespace persistence.interfaces
{
    public interface IFestivalRepository : IRepository<int, Festival>
    {
        ICollection<Festival> GetAllOnDate(DateTime date);
    }
}