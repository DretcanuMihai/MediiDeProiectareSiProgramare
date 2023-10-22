package ticketseller.model.validators.implementations;


import ticketseller.model.entities.User;
import ticketseller.model.validators.ValidationException;
import ticketseller.model.validators.interfaces.IUserValidator;

public class UserValidator implements IUserValidator {
    @Override
    public void validate(User user) throws ValidationException {
    }

    @Override
    public void validateForLogin(String username, String password) throws ValidationException {
        String error = "";
        if (username == null)
            error += "Username is null;\n";
        if (password == null)
            error += "Password is null;\n";
        if (!error.equals(""))
            throw new ValidationException(error);
    }
}
