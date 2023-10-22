package ticketseller.persistence.implementations;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.persistence.interfaces.ITicketRepository;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class TicketORMRepository implements ITicketRepository {
    private static final Logger logger = LogManager.getLogger();

    private final SessionFactory sessionFactory;

    public TicketORMRepository(SessionFactory sessionFactory) {
        this.sessionFactory = sessionFactory;
        logger.info("Initializing TicketORMRepository with sessionFactory: {} ",
                sessionFactory);
    }

    @Override
    public void add(Ticket entity) {
        logger.traceEntry("Saving festival {}", entity);
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();
                session.persist(entity);
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la inserare "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        logger.traceExit();
    }

    @Override
    public void delete(Ticket entity) {
        logger.traceEntry("Deleting festival {}", entity);
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();
                session.remove(entity);
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la stergere "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        logger.traceExit();
    }

    @Override
    public void update(Ticket entity, Integer id) {
        logger.traceEntry("Updating festival with id: {} with info: {}", id, entity);
        try(Session session = sessionFactory.openSession()){
            Transaction tx=null;
            try{
                entity.setId(id);
                tx = session.beginTransaction();
                session.merge(entity);
                tx.commit();

            } catch(RuntimeException ex){
                System.err.println("Eroare la update "+ex);
                if (tx!=null)
                    tx.rollback();
            }
        }
        logger.traceExit();
    }

    @Override
    public Ticket findByID(Integer id) {
        logger.traceEntry("Searching festival with id: {}", id);
        Ticket ticket = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                ticket = session.get( Ticket.class, id );

                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        logger.traceExit(ticket);
        return ticket;
    }

    @Override
    public Iterable<Ticket> findAll() {
        logger.traceEntry();
        Iterable<Ticket> tickets=getAll();
        logger.traceExit(tickets);
        return tickets;
    }

    @Override
    public Collection<Ticket> getAll() {
        logger.traceEntry();
        List<Ticket> tickets = new ArrayList<>();
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                tickets = session.createQuery("from Ticket as t", Ticket.class)
                        .list();
                System.out.println(tickets.size() + " festival(s) found:");
                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        logger.traceExit(tickets);
        return tickets;
    }
}
