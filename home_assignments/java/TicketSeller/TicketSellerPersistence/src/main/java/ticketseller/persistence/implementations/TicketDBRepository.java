package ticketseller.persistence.implementations;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.persistence.JdbcUtils;
import ticketseller.persistence.interfaces.ITicketRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class TicketDBRepository implements ITicketRepository {

    private static final Logger logger = LogManager.getLogger();
    private final JdbcUtils dbUtils;

    public TicketDBRepository(Properties props) {
        logger.info("Initializing CarsDBRepository with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }


    @Override
    public void add(Ticket ticket) {
        logger.traceEntry("Saving ticket {}", ticket);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("insert into Tickets(buyer_name, festival_id, number_of_spots) values (?,?,?)")) {
            preparedStatement.setString(1, ticket.getBuyerName());
            preparedStatement.setInt(2, ticket.getFestival().getId());
            preparedStatement.setInt(3, ticket.getNumberOfSpots());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Ticket ticket) {
        logger.traceEntry("Deleting ticket {}", ticket);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("delete from Tickets where id=(?)")) {
            preparedStatement.setInt(1, ticket.getId());
            int result = preparedStatement.executeUpdate();
            logger.trace("Deleted {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Ticket ticket, Integer id) {
        logger.traceEntry("Updating festival with id: {} with info: {}", id, ticket);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("update Tickets" +
                " set id=(?),buyer_name=(?),festival_id=(?),number_of_spots=(?)" +
                " where id=(?)")) {
            preparedStatement.setInt(1, ticket.getId());
            preparedStatement.setString(2, ticket.getBuyerName());
            preparedStatement.setInt(3, ticket.getFestival().getId());
            preparedStatement.setInt(4, ticket.getNumberOfSpots());
            preparedStatement.setInt(5, id);
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }


    /**
     * extracts a festival from a result set
     *
     * @param resultSet - said result set
     * @return said festival
     * @throws SQLException - if any error occurs
     */
    private Festival extractFestivalFromSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("Extracting festival from set:{}", resultSet);
        Integer id = resultSet.getInt("T.festival_id");
        String artistName = resultSet.getString("F.artist_name");
        LocalDateTime date = resultSet.getTimestamp("F.date").toLocalDateTime();
        String place = resultSet.getString("F.place");
        Integer availableSpots = resultSet.getInt("F.available_spots");
        Integer soldSpots = resultSet.getInt("F.sold_spots");
        Festival festival = new Festival(id, artistName, date, place, availableSpots, soldSpots);
        logger.traceExit(festival);
        return festival;
    }

    /**
     * extracts a ticket from a result set
     *
     * @param resultSet - said set
     * @return said ticket
     * @throws SQLException - if any error occurs
     */
    private Ticket extractTicketFromSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("Extracting ticket from set:{}", resultSet);
        Integer id = resultSet.getInt("T.id");
        String buyerName = resultSet.getString("T.buyer_name");
        Integer spots = resultSet.getInt("T.number_of_spots");
        Ticket ticket = new Ticket(id, buyerName, extractFestivalFromSet(resultSet), spots);
        logger.traceExit(ticket);
        return ticket;
    }

    /**
     * finds a festival by a given id
     *
     * @param id - said id
     * @return the festival if it exists, null otherwise
     */
    private Festival findFestivalByID(Integer id) {
        logger.traceEntry("Searching festival with id: {}", id);
        Festival festival = null;
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from Festivals where id=(?)")) {
            preparedStatement.setInt(1, id);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                if (resultSet.next()) {
                    festival = extractFestivalFromSet(resultSet);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(festival);
        return festival;
    }

    @Override
    public Ticket findByID(Integer id) {
        logger.traceEntry("Searching ticket with id: {}", id);
        Ticket ticket = null;
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select T.id, T.buyer_name," +
                " T.festival_id, T.number_of_spots, F.artist_name, F.date,F.place,F.available_spots,F.sold_spots" +
                " from Tickets T inner join Festivals F on F.id = T.festival_id where T.id=(?)")) {
            preparedStatement.setInt(1, id);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                if (resultSet.next()) {
                    ticket = extractTicketFromSet(resultSet);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(ticket);
        return ticket;
    }

    @Override
    public Iterable<Ticket> findAll() {
        logger.traceEntry();
        Iterable<Ticket> tickets = getAll();
        logger.traceExit(tickets);
        return tickets;
    }

    @Override
    public Collection<Ticket> getAll() {
        logger.traceEntry();
        List<Ticket> tickets = new ArrayList<>();
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select T.id, T.buyer_name," +
                " T.festival_id, T.number_of_spots, F.artist_name, F.date,F.place,F.available_spots,F.sold_spots" +
                " from Tickets T inner join Festivals F on F.id = T.festival_id")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    Ticket ticket = extractTicketFromSet(resultSet);
                    tickets.add(ticket);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(tickets);
        return tickets;
    }
}
