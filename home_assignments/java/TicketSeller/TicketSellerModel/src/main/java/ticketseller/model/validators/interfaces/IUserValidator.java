package ticketseller.model.validators.interfaces;


import ticketseller.model.entities.User;
import ticketseller.model.validators.ValidationException;

public interface IUserValidator extends IValidator<String, User> {

    /**
     * validates credentials for login functions
     *
     * @param username - said username
     * @param password - said password
     * @throws ValidationException if credentials are null
     */
    void validateForLogin(String username, String password) throws ValidationException;
}
