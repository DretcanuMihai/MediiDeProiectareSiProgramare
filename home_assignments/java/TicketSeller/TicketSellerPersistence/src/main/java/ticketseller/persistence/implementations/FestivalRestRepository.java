package ticketseller.persistence.implementations;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import ticketseller.model.entities.Festival;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class FestivalRestRepository {

    private final SessionFactory sessionFactory;

    public FestivalRestRepository(SessionFactory sessionFactory) {
        this.sessionFactory = sessionFactory;
    }

    public Festival create(Festival entity) {
        Festival toReturn = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();
                Integer id = (Integer) session.save(entity);
                tx.commit();
                entity.setId(id);
                toReturn = entity;
            } catch (RuntimeException ex) {
                System.err.println("Eroare la inserare " + ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return toReturn;
    }

    public Festival readById(Integer id) {
        Festival festival = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                Festival aux;
                transaction = session.beginTransaction();
                aux = session.get(Festival.class, id);
                transaction.commit();
                festival = aux;
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        return festival;
    }

    public Collection<Festival> readAll() {
        List<Festival> festivals = new ArrayList<>();
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                festivals = session.createQuery("from Festival as f", Festival.class)
                        .list();
                System.out.println(festivals.size() + " festival(s) found:");
                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        return festivals;
    }

    public Festival update(Festival entity, Integer id) {
        Festival toReturn = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                entity.setId(id);
                Festival aux;
                tx = session.beginTransaction();
                aux = session.get(Festival.class, id);
                session.merge(entity);
                tx.commit();
                toReturn = aux;

            } catch (RuntimeException ex) {
                System.err.println("Eroare la update " + ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return toReturn;
    }

    public Festival delete(Festival entity) {
        Festival toReturn = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                Festival aux;
                tx = session.beginTransaction();
                aux = session.get(Festival.class, entity.getId());
                session.delete(aux);
                tx.commit();
                toReturn = aux;
            } catch (RuntimeException ex) {
                System.err.println("Eroare la stergere " + ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return toReturn;
    }
}
