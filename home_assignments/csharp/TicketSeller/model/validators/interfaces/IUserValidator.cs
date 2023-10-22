using System;
using model.entities;

namespace model.validators.interfaces;

public interface IUserValidator:IValidator<string,User>
{
    /// <summary>
    /// validates an user for login
    /// </summary>
    /// <param name="username">user's username</param>
    /// <param name="password">user's password</param>
    /// <exception cref="ValidationException">if username or password are null</exception>
    void ValidateForLogin(String username, String password);
}