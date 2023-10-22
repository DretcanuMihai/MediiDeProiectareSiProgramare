package ticketseller.services.interfaces;

import ticketseller.model.entities.Ticket;

public interface ITicketObserver {
    void updateTicketSold(Ticket ticket);
}
