using System;
using System.Collections.Generic;
using model.entities;

namespace services.interfaces;

public interface IFestivalService
{
    ICollection<Festival> GetAllFestivals();
    
    ICollection<Festival> GetAllFestivalsOnDate(DateTime date);
    
    Ticket SellTicket(int festivalId, String buyerName, int spots);

}