using model.entities;

namespace services.interfaces;

public interface ITicketObserver
{
    void UpdateTicketSold(Ticket ticket);
}