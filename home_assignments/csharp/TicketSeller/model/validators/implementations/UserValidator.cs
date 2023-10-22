using System;
using model.entities;
using model.validators.interfaces;

namespace model.validators.implementations;

public class UserValidator:IUserValidator
{
    public void Validate(User entity)
    {
        throw new NotImplementedException();
    }
    public void ValidateForLogin(string username, string password)
    {
        String error = "";
        if (username == null)
            error += "Username is null;\n";
        if (password == null)
            error += "Password is null;\n";
        if (!error.Equals(""))
            throw new ValidationException(error);
    }
}