package ticketseller.persistence.interfaces;


import ticketseller.model.entities.Festival;

import java.time.LocalDate;
import java.util.Collection;

public interface IFestivalRepository extends IRepository<Integer, Festival> {

    Collection<Festival> getAllOnDate(LocalDate date);
}
