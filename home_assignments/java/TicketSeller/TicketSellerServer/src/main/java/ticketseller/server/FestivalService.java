package ticketseller.server;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.validators.interfaces.IFestivalValidator;
import ticketseller.model.validators.interfaces.ITicketValidator;
import ticketseller.persistence.interfaces.IFestivalRepository;
import ticketseller.persistence.interfaces.ITicketRepository;
import ticketseller.services.ServiceException;
import ticketseller.model.validators.ValidationException;
import ticketseller.services.interfaces.IFestivalService;

import java.time.LocalDate;
import java.util.Collection;

public class FestivalService implements IFestivalService {

    private static final Logger logger = LogManager.getLogger();

    private final IFestivalValidator festivalValidator;
    private final IFestivalRepository festivalRepository;
    private final ITicketValidator ticketValidator;
    private final ITicketRepository ticketRepository;

    public FestivalService(IFestivalRepository festivalRepository, IFestivalValidator festivalValidator,
                           ITicketRepository ticketRepository, ITicketValidator ticketValidator) {
        this.festivalValidator = festivalValidator;
        this.festivalRepository = festivalRepository;
        this.ticketValidator = ticketValidator;
        this.ticketRepository = ticketRepository;
        logger.info("Instantiated FestivalService with Festival repo:{}, Festival validator:{}," +
                        " Ticket repo:{}, Ticket validator:{}",
                festivalRepository, festivalValidator, ticketRepository, ticketValidator);
    }

    @Override
    public Collection<Festival> getAllFestivals() {
        logger.traceEntry();
        Collection<Festival> toReturn = festivalRepository.getAll();
        logger.traceExit(toReturn);
        return toReturn;
    }

    @Override
    public Collection<Festival> getAllFestivalsOnDate(LocalDate date) throws ServiceException {
        logger.traceEntry("getting all festivals on date:{}", date);
        try {
            festivalValidator.validateDate(date);
        } catch (ValidationException e) {
            logger.error(e);
            throw new ServiceException(e.getMessage());
        }
        Collection<Festival> toReturn = festivalRepository.getAllOnDate(date);
        logger.traceExit(toReturn);
        return toReturn;
    }

    @Override
    public Ticket sellTicket(Integer festivalID, String buyerName, Integer spots)
            throws ServiceException{
        logger.traceEntry("selling ticket to festival:{} for buyer:{} with spots:{}",
                festivalID, buyerName, spots);
        try {
            festivalValidator.validateID(festivalID);
        } catch (ValidationException e) {
            logger.error(e);
            throw new ServiceException(e.getMessage());
        }
        Festival festival = festivalRepository.findByID(festivalID);
        if (festival == null) {
            String error = "No festival with given id found!;\n";
            logger.error(error);
            throw new ServiceException(error);
        }
        Ticket ticket = new Ticket(null,buyerName, festival, spots);
        try {
            ticketValidator.validate(ticket);
        } catch (ValidationException e) {
            logger.error(e);
            throw new ServiceException(e.getMessage());
        }
        ticketRepository.add(ticket);
        festival.setSoldSpots(festival.getSoldSpots() + spots);
        festivalRepository.update(festival, festival.getId());
        logger.traceExit(festival);
        return ticket;
    }
}
