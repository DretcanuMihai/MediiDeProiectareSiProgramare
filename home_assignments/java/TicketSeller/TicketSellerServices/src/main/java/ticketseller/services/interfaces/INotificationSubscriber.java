package ticketseller.services.interfaces;

import ticketseller.model.entities.Ticket;

public interface INotificationSubscriber {
    void ticketBought(Ticket ticket);
}
