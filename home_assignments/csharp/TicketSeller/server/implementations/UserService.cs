using System;
using log4net;
using model.entities;
using model.validators;
using model.validators.interfaces;
using persistence.interfaces;
using services;
using services.interfaces;

namespace server.implementations;

public class UserService : IUserService
{
    private static readonly ILog Logger = LogManager.GetLogger("UserService");


    private readonly IUserValidator _userValidator;
    private readonly IUserRepository _userRepository;

    /**
     * constructs a user service based on given validator and repository
     *
     * @param userValidator  - said validator
     * @param userRepository - said repository
     */
    public UserService(IUserValidator userValidator, IUserRepository userRepository)
    {
        this._userValidator = userValidator;
        this._userRepository = userRepository;
        Logger.InfoFormat("Instantiated UserService with repo:{0} and validator:{1}", userRepository, userValidator);
    }

    public void Login(string username, string password)
    {
        Logger.InfoFormat("trying to login with username:{0} and password:{1}", username, password);
        try {
            _userValidator.ValidateForLogin(username, password);
        } catch (ValidationException e) {
            Logger.Error(e);
            throw e;
        }
        User user = _userRepository.FindById(username);
        Boolean toReturn = user != null && user.Password.Equals(password);
        if(!toReturn){
            throw new ServiceException("No user with given credentials");
        }
        Logger.Info("Exiting login");
    }
}