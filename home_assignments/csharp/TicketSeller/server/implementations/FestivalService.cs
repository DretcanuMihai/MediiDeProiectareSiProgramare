using System;
using System.Collections.Generic;
using log4net;
using model.entities;
using model.validators;
using model.validators.interfaces;
using persistence.interfaces;
using services;
using services.interfaces;

namespace server.implementations;

public class FestivalService : IFestivalService
{
    private static readonly ILog Logger = LogManager.GetLogger("FestivalService");

    private readonly IFestivalValidator _festivalValidator;
    private readonly IFestivalRepository _festivalRepository;
    private readonly ITicketValidator _ticketValidator;
    private readonly ITicketRepository _ticketRepository;

    public FestivalService(IFestivalRepository festivalRepository, IFestivalValidator festivalValidator,
        ITicketRepository ticketRepository, ITicketValidator ticketValidator)
    {
        this._festivalValidator = festivalValidator;
        this._festivalRepository = festivalRepository;
        this._ticketValidator = ticketValidator;
        this._ticketRepository = ticketRepository;
        Logger.InfoFormat("Instantiated FestivalService with Festival repo:{0}, Festival validator:{1}," +
                          " Ticket repo:{2}, Ticket validator:{3}",
            festivalRepository, festivalValidator, ticketRepository, ticketValidator);
    }

    public ICollection<Festival> GetAllFestivals()
    {
        Logger.Info("Entered get all");
        ICollection<Festival> toReturn = _festivalRepository.GetAll();
        Logger.InfoFormat("Exit with:{0}", toReturn);
        return toReturn;
    }

    public ICollection<Festival> GetAllFestivalsOnDate(DateTime date)
    {
        Logger.InfoFormat("getting all festivals on date:{0}", date);
        try
        {
            _festivalValidator.ValidateDate(date);
        }
        catch (ValidationException e)
        {
            Logger.Error(e);
            throw new ServiceException(e.Message);
        }

        ICollection<Festival> toReturn = _festivalRepository.GetAllOnDate(date);
        Logger.InfoFormat("exiting with:{0}", toReturn);
        return toReturn;
    }

    public Ticket SellTicket(int festivalId, string buyerName, int spots)
    {
        Logger.InfoFormat("selling ticket to festival:{0} for buyer:{1} with spots:{2}",
            festivalId, buyerName, spots);
        try
        {
            _festivalValidator.ValidateId(festivalId);
        }
        catch (ValidationException e)
        {
            Logger.Error(e);
            throw new ServiceException(e.Message);
        }

        Festival festival = _festivalRepository.FindById(festivalId);
        if (festival == null)
        {
            String error = "No festival with given id found!;\n";
            Logger.Error(error);
            throw new ServiceException(error);
        }

        Ticket ticket = new Ticket(0, buyerName, festival, spots);
        try
        {
            _ticketValidator.Validate(ticket);
        }
        catch (ValidationException e)
        {
            Logger.Error(e);
            throw new ServiceException(e.Message);
        }

        _ticketRepository.Add(ticket);
        festival.SoldSpots += +spots;
        _festivalRepository.Update(festival, festival.Id);
        Logger.InfoFormat("Exiting buy method with:{0}",ticket);
        return ticket;
    }
}