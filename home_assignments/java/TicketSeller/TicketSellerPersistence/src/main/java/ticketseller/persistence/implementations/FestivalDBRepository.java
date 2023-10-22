package ticketseller.persistence.implementations;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ticketseller.model.entities.Festival;
import ticketseller.persistence.JdbcUtils;
import ticketseller.persistence.interfaces.IFestivalRepository;

import java.sql.*;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class FestivalDBRepository implements IFestivalRepository {

    private static final Logger logger = LogManager.getLogger();
    private final JdbcUtils dbUtils;

    public FestivalDBRepository(Properties props) {
        logger.info("Initializing CarsDBRepository with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void add(Festival festival) {
        logger.traceEntry("Saving festival {}", festival);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("insert into Festivals(artist_name, date, place, available_spots, sold_spots) values(?,?,?,?,?)")) {
            preparedStatement.setString(1, festival.getArtistName());
            preparedStatement.setString(2, Timestamp.valueOf(festival.getDateTime()).toString());
            preparedStatement.setString(3, festival.getPlace());
            preparedStatement.setInt(4, festival.getAvailableSpots());
            preparedStatement.setInt(5, festival.getSoldSpots());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Festival festival) {
        logger.traceEntry("Deleting festival {}", festival);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("delete from Festivals where id=(?)")) {
            preparedStatement.setInt(1, festival.getId());
            int result = preparedStatement.executeUpdate();
            logger.trace("Deleted {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Festival festival, Integer id) {
        logger.traceEntry("Updating festival with id: {} with info: {}", id, festival);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("update Festivals" +
                " set id=(?),artist_name=(?),date=(?),place=(?),available_spots=(?),sold_spots=(?)" +
                " where id=(?)")) {
            preparedStatement.setInt(1, festival.getId());
            preparedStatement.setString(2, festival.getArtistName());
            preparedStatement.setString(3, Timestamp.valueOf(festival.getDateTime()).toString());
            preparedStatement.setString(4, festival.getPlace());
            preparedStatement.setInt(5, festival.getAvailableSpots());
            preparedStatement.setInt(6, festival.getSoldSpots());
            preparedStatement.setInt(7, id);
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
     * @param resultSet said set
     * @return said festival
     * @throws SQLException - if any error occurs
     */
    private Festival extractFestivalFromSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("Extracting festival from set:{}", resultSet);
        Integer id = resultSet.getInt("id");
        String artistName = resultSet.getString("artist_name");
        LocalDateTime date = resultSet.getTimestamp("date").toLocalDateTime();
        String place = resultSet.getString("place");
        Integer availableSpots = resultSet.getInt("available_spots");
        Integer soldSpots = resultSet.getInt("sold_spots");
        Festival festival = new Festival(id, artistName, date, place, availableSpots, soldSpots);
        logger.traceExit(festival);
        return festival;
    }


    @Override
    public Festival findByID(Integer id) {
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
    public Iterable<Festival> findAll() {
        logger.traceEntry();
        Iterable<Festival> festivals = getAll();
        logger.traceExit(festivals);
        return festivals;
    }

    @Override
    public Collection<Festival> getAll() {
        logger.traceEntry();
        List<Festival> festivals = new ArrayList<>();
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from Festivals")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    Festival festival = extractFestivalFromSet(resultSet);
                    festivals.add(festival);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(festivals);
        return festivals;
    }

    @Override
    public Collection<Festival> getAllOnDate(LocalDate date) {
        logger.traceEntry("search all festivals on date:{}", date);
        List<Festival> festivals = new ArrayList<>();
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from Festivals where DATE(date)=(?)")) {
            preparedStatement.setString(1, date.toString());
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    Festival festival = extractFestivalFromSet(resultSet);
                    festivals.add(festival);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(festivals);
        return festivals;
    }
}
