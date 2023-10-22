using System;

namespace services.interfaces;

public interface IUserService
{
    void Login(String username, String password);
}