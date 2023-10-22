package ticketseller.model.validators.interfaces;


import ticketseller.model.entities.Festival;
import ticketseller.model.validators.ValidationException;

import java.time.LocalDate;

public interface IFestivalValidator extends IValidator<Integer, Festival> {
    /**
     * validates a festival's id
     *
     * @param id - said id
     * @throws ValidationException if festival's id is null
     */
    void validateID(Integer id) throws ValidationException;

    /**
     * validates a date
     *
     * @param date - said date
     * @throws ValidationException if date is null
     */
    void validateDate(LocalDate date) throws ValidationException;
}
