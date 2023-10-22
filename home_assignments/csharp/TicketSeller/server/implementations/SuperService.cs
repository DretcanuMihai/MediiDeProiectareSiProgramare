using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using model.entities;
using services;
using services.interfaces;

namespace server.implementations;

public class SuperService : ISuperService
{
    private static readonly ILog Logger = LogManager.GetLogger("SuperService");
    private readonly IFestivalService _festivalService;
    private readonly IUserService _userService;
    private readonly IDictionary<String, ITicketObserver> _loggedClients;

    public SuperService(IFestivalService festivalService, IUserService userService)
    {
        _festivalService = festivalService;
        _userService = userService;
        _loggedClients = new ConcurrentDictionary<String, ITicketObserver>();
        Logger.InfoFormat("Instantiated SuperService with Festival service:{0}, User service:{1}",
            festivalService, userService);
    }

    public void Login(string username, string password, ITicketObserver observer)
    {
        Logger.InfoFormat("trying to login with username:{0} and password:{1}", username, password);
        _userService.Login(username, password);
        if (_loggedClients.ContainsKey(username))
            throw new ServiceException("User already logged in.");
        _loggedClients[username] = observer;
        Logger.Info("Exiting Login");
    }

    public void Logout(string username)
    {
        Logger.InfoFormat("trying to logout with username:{0} ", username);
        bool ticketObserver=_loggedClients.Remove(username);
        if(ticketObserver==false){
            Logger.Error("User not logged in!\n");
            throw new ServiceException("User not logged in!\n");
        }
        Logger.InfoFormat("Exiting logout");
    }

    public ICollection<Festival> GetAllFestivals()
    {
        Logger.Info("Entering get all festivals");
        ICollection<Festival> toReturn = _festivalService.GetAllFestivals();
        Logger.InfoFormat("Exiting with:{0}", toReturn);
        return toReturn;
    }

    public ICollection<Festival> GetAllFestivalsOnDate(DateTime date)
    {
        Logger.InfoFormat("getting all festivals on date:{0}", date);
        ICollection<Festival> toReturn = _festivalService.GetAllFestivalsOnDate(date);
        Logger.InfoFormat("Exiting with:{0}", toReturn);
        return toReturn;
    }

    private void NotifyClients(Ticket ticket)
    {
        IEnumerable<ITicketObserver> users=_loggedClients.Values;
        foreach(ITicketObserver observer in users){
            Task.Run(() => observer.UpdateTicketSold(ticket));
        }
    }
    
    public void SellTicket(int festivalId, string buyerName, int spots)
    {
        Logger.InfoFormat("selling ticket to festival:{0} for buyer:{1} with spots:{2}",
            festivalId, buyerName, spots);
        Ticket ticket = _festivalService.SellTicket(festivalId, buyerName, spots);
        NotifyClients(ticket);
        Logger.Info("Exiting sell ticket");
    }
}