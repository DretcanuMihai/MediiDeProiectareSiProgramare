package ticketseller.services.interfaces;

import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.services.ServiceException;

import java.time.LocalDate;
import java.util.Collection;

public interface IFestivalService {

    Collection<Festival> getAllFestivals();

    Collection<Festival> getAllFestivalsOnDate(LocalDate date) throws ServiceException;

    Ticket sellTicket(Integer festivalID, String buyerName, Integer spots) throws ServiceException;
}
