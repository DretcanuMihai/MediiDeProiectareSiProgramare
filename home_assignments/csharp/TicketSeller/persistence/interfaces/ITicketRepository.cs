using model.entities;

namespace persistence.interfaces
{
    public interface ITicketRepository : IRepository<int, Ticket>
    {
    }
}