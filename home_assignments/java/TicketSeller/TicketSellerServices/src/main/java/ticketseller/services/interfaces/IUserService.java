package ticketseller.services.interfaces;


import ticketseller.services.ServiceException;

public interface IUserService {

    void login(String username, String password) throws ServiceException;
}
