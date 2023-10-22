package ticketseller.services.interfaces;

import ticketseller.model.entities.Festival;
import ticketseller.services.ServiceException;

import java.time.LocalDate;
import java.util.Collection;

public interface ISuperService {

    void login(String username, String password,ITicketObserver observer) throws ServiceException;

    void logout(String username) throws ServiceException;

    Collection<Festival> getAllFestivals()throws ServiceException;

    Collection<Festival> getAllFestivalsOnDate(LocalDate date) throws ServiceException;

    void sellTicket(Integer festivalID, String buyerName, Integer spots) throws ServiceException;

}
