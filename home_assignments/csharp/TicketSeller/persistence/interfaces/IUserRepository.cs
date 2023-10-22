using model.entities;

namespace persistence.interfaces
{
    public interface IUserRepository : IRepository<string, User>
    {
    }
}