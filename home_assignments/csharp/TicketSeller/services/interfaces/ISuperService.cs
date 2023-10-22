using System;
using System.Collections.Generic;
using model.entities;
using model.validators;

namespace services.interfaces;

public interface ISuperService
{
    void Login(String username, String password,ITicketObserver observer);
    void Logout(String username);
    
    ICollection<Festival> GetAllFestivals();
    
    ICollection<Festival> GetAllFestivalsOnDate(DateTime date);
    
    void SellTicket(int festivalId, String buyerName, int spots);
}