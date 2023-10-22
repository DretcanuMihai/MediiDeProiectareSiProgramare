package ticketseller.model.validators.implementations;


import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.validators.ValidationException;
import ticketseller.model.validators.interfaces.ITicketValidator;

public class TicketValidator implements ITicketValidator {
    @Override
    public void validate(Ticket ticket) throws ValidationException {
        String buyerName = ticket.getBuyerName();
        Integer spots = ticket.getNumberOfSpots();
        Festival festival = ticket.getFestival();
        String error = "";
        if (buyerName == null)
            error += "buyerName is null;\n";
        else {
            if (buyerName.equals("")) {
                error += "Buyer Name should be non empty!;\n";
            }
        }
        if (spots == null)
            error += "spots is null;\n";
        else {
            if (spots <= 0) {
                error += "Spots should be a positive number!;\n";
            }
            if (spots > festival.getRemainingSpots()) {
                error += "Not enough available spots!;\n";
            }
        }
        if (!error.equals("")) {
            throw new ValidationException(error);
        }
    }
}
