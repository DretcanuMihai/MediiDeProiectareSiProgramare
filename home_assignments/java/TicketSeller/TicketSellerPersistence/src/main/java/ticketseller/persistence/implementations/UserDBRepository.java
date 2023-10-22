package ticketseller.persistence.implementations;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ticketseller.model.entities.User;
import ticketseller.persistence.JdbcUtils;
import ticketseller.persistence.interfaces.IUserRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class UserDBRepository implements IUserRepository {

    private static final Logger logger = LogManager.getLogger();
    private final JdbcUtils dbUtils;

    public UserDBRepository(Properties props) {
        logger.info("Initializing CarsDBRepository with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void add(User user) {
        logger.traceEntry("Saving user {}", user);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("insert into Users(username,password) values(?,?)")) {
            preparedStatement.setString(1, user.getUsername());
            preparedStatement.setString(2, user.getPassword());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    @Override
    public void delete(User user) {
        logger.traceEntry("Deleting user {}", user);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("delete from Users where username=(?)")) {
            preparedStatement.setString(1, user.getUsername());
            int result = preparedStatement.executeUpdate();
            logger.trace("Deleted {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    @Override
    public void update(User user, String username) {
        logger.traceEntry("Updating user with username: {} with info: {}", username, user);
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("update Users set username=(?),password=(?) where username=(?)")) {
            preparedStatement.setString(1, user.getUsername());
            preparedStatement.setString(2, user.getPassword());
            preparedStatement.setString(3, username);
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit();
    }

    /**
     * extracts a user from a result set
     *
     * @param resultSet said set
     * @return said user
     * @throws SQLException - if any error occurs
     */
    private User extractUserFromSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("Extracting user from set:{}", resultSet);
        String username = resultSet.getString("username");
        String password = resultSet.getString("password");
        User user = new User(username, password);
        logger.traceExit(user);
        return user;
    }

    @Override
    public User findByID(String username) {
        logger.traceEntry("Searching user with username: {}", username);
        User user = null;
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from Users where username=(?)")) {
            preparedStatement.setString(1, username);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                if (resultSet.next()) {
                    user = extractUserFromSet(resultSet);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(user);
        return user;
    }

    @Override
    public Iterable<User> findAll() {
        logger.traceEntry();
        Iterable<User> users = getAll();
        logger.traceExit(users);
        return users;
    }

    @Override
    public Collection<User> getAll() {
        logger.traceEntry();
        List<User> users = new ArrayList<>();
        Connection connection = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from Users")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    User user = extractUserFromSet(resultSet);
                    users.add(user);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB:" + e);
        }
        logger.traceExit(users);
        return users;
    }
}
