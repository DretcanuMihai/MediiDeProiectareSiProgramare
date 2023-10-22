package ticketseller.server;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ticketseller.model.entities.User;
import ticketseller.model.validators.interfaces.IUserValidator;
import ticketseller.persistence.interfaces.IUserRepository;
import ticketseller.model.validators.ValidationException;
import ticketseller.services.ServiceException;
import ticketseller.services.interfaces.IUserService;

public class UserService implements IUserService {

    private static final Logger logger = LogManager.getLogger();

    private final IUserValidator userValidator;
    private final IUserRepository userRepository;

    /**
     * constructs a user service based on given validator and repository
     *
     * @param userValidator  - said validator
     * @param userRepository - said repository
     */
    public UserService(IUserValidator userValidator, IUserRepository userRepository) {
        this.userValidator = userValidator;
        this.userRepository = userRepository;
        logger.info("Instantiated UserService with repo:{} and validator:{}", userRepository, userValidator);
    }

    @Override
    public void login(String username, String password) throws ServiceException {
        logger.traceEntry("trying to login with username:{} and password:{}", username, password);
        try {
            userValidator.validateForLogin(username, password);
        } catch (ValidationException e) {
            logger.error(e);
            throw new ServiceException(e.getMessage());
        }
        User user = userRepository.findByID(username);
        boolean toReturn = user != null && user.getPassword().equals(password);
        if(!toReturn){
            throw new ServiceException("No user with given credentials");
        }
        logger.traceExit();
    }
}
