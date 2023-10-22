using System;
using model.entities;
using model.validators.interfaces;

namespace model.validators.implementations;

public class TicketValidator : ITicketValidator
{
    public void Validate(Ticket ticket)
    {
        String buyerName = ticket.BuyerName;
        int spots = ticket.NumberOfSpots;
        Festival festival = ticket.Festival;
        String error = "";
        if (buyerName == null)
            error += "buyerName is null;\n";
        else
        {
            if (buyerName.Equals(""))
            {
                error += "Buyer Name should be non empty!;\n";
            }
        }

        if (spots == null)
            error += "spots is null;\n";
        else
        {
            if (spots <= 0)
            {
                error += "Spots should be a positive number!;\n";
            }

            if (spots > festival.RemainingSpots)
            {
                error += "Not enough available spots!;\n";
            }
        }

        if (!error.Equals(""))
        {
            throw new ValidationException(error);
        }
    }
}