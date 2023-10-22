package ticketseller.server;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.jms.core.JmsOperations;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.entities.TicketNotif;
import ticketseller.model.entities.User;
import ticketseller.services.ServiceException;
import ticketseller.services.interfaces.*;

import java.time.LocalDate;
import java.util.Collection;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class AMSSuperService implements IAMSSuperService {
    private static final Logger logger = LogManager.getLogger();
    private final IFestivalService festivalService;
    private final IUserService userService;
    private final Map<String, User> loggedClients;

    private final JmsOperations jmsOperations;

    public AMSSuperService(IFestivalService festivalService, IUserService userService, JmsOperations jmsOperations) {
        this.festivalService = festivalService;
        this.userService = userService;
        this.jmsOperations = jmsOperations;
        loggedClients = new ConcurrentHashMap<>();
        logger.info("Instantiated SuperService with Festival service:{}, User service:{}",
                festivalService, userService);
    }

    @Override
    public synchronized void login(String username, String password) throws ServiceException {
        logger.traceEntry("trying to login with username:{} and password:{}", username, password);
        userService.login(username, password);
        if (loggedClients.get(username) != null) {
            logger.error("User already logged in.");
            throw new ServiceException("User already logged in.");
        }
        loggedClients.put(username, new User(username,password));
        logger.traceExit();
    }

    @Override
    public synchronized void logout(String username) throws ServiceException {
        logger.traceEntry("trying to logout with username:{} ", username);
        User user = loggedClients.remove(username);
        if (user == null) {
            logger.error("User not logged in!\n");
            throw new ServiceException("User not logged in!\n");
        }
        logger.traceExit();
    }

    @Override
    public synchronized Collection<Festival> getAllFestivals() {
        logger.traceEntry();
        Collection<Festival> toReturn = festivalService.getAllFestivals();
        logger.traceExit(toReturn);
        return toReturn;
    }

    @Override
    public synchronized Collection<Festival> getAllFestivalsOnDate(LocalDate date) throws ServiceException {
        logger.traceEntry("getting all festivals on date:{}", date);
        Collection<Festival> toReturn = festivalService.getAllFestivalsOnDate(date);
        logger.traceExit(toReturn);
        return toReturn;
    }

    private final int defaultThreadsNo = 5;

    private void notifyUsers(Ticket ticket) {
        logger.traceEntry("Entered with {}", ticket);
        TicketNotif ticketNotif=new TicketNotif();
        ticketNotif.setId(ticket.getId());
        ticketNotif.setBuyerName(ticket.getBuyerName());
        ticketNotif.setFestivalId(ticket.getFestival().getId());
        ticketNotif.setNumberOfSpots(ticket.getNumberOfSpots());
        jmsOperations.convertAndSend(ticketNotif);
        logger.traceExit();
    }

    @Override
    public synchronized void sellTicket(Integer festivalID, String buyerName, Integer spots) throws ServiceException {
        logger.traceEntry("selling ticket to festival:{} for buyer:{} with spots:{}",
                festivalID, buyerName, spots);
        Ticket ticket = festivalService.sellTicket(festivalID, buyerName, spots);
        notifyUsers(ticket);
        logger.traceExit();
    }
}
