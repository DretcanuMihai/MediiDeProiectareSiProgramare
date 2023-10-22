package ticketseller.persistence.interfaces;

import ticketseller.model.entities.Identifiable;

import java.util.Collection;

public interface IRepository<ID, E extends Identifiable<ID>> {

    void add(E entity);

    void delete(E entity);

    void update(E entity, ID id);

    E findByID(ID id);

    Iterable<E> findAll();

    Collection<E> getAll();

}
