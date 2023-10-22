using System;
using System.Collections.Generic;
using model.entities;
using services.interfaces;

namespace client;

public class MainController: ITicketObserver
{
    public event EventHandler<TicketEventArgs> updateEvent;
    public readonly ISuperService server;
    private String Username;

    public MainController(ISuperService server)
    {
        this.server = server;
    }

    public void login(String username, String password)
    {
        server.Login(username,password,this);
        Console.WriteLine("Login succeeded ....");
        Username = username;
        Console.WriteLine("Current user {0}", Username);
    }

    public void UpdateTicketSold(Ticket ticket)
    {
        TicketEventArgs userArgs=new TicketEventArgs(TicketEvent.TicketBought,ticket);
        Console.WriteLine("Ticket Bought");
        OnUserEvent(userArgs);
    }

    
    public void buyTicket(int festivalId,string buyerName,int spots)
    {
        server.SellTicket(festivalId,buyerName,spots);
        Console.WriteLine("Buying Ticket");
    }
    public void logout()
    {
        Console.WriteLine("Ctrl logout");
        server.Logout(Username);
        Username = null;
    }
    protected virtual void OnUserEvent(TicketEventArgs e)
    {
        if (updateEvent == null) return;
        updateEvent(this, e);
        Console.WriteLine("Update Event called");
    }
    public ICollection<Festival> getFestivals()
    {
        return server.GetAllFestivals();
    }
    public ICollection<Festival> getFestivalsOnDate(DateTime date)
    {
        return server.GetAllFestivalsOnDate(date);
    }
}