package ticketseller.persistence.implementations;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import ticketseller.model.entities.Festival;
import ticketseller.persistence.interfaces.IFestivalRepository;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class FestivalORMRepository implements IFestivalRepository {

    private static final Logger logger = LogManager.getLogger();

    private final SessionFactory sessionFactory;

    public FestivalORMRepository(SessionFactory sessionFactory) {
        this.sessionFactory = sessionFactory;
        logger.info("Initializing FestivalORMRepository with sessionFactory: {} ",
                sessionFactory);
    }

    @Override
    public Collection<Festival> getAllOnDate(LocalDate date) {
        logger.traceEntry("search all festivals on date:{}", date);
        List<Festival> festivals = new ArrayList<>();
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                festivals = session.createQuery("from Festival as f where DATE(f.dateTimeSql)=:date", Festival.class)
                        .setParameter("date", date.toString())
                        .list();
                System.out.println(festivals.size() + " festival(s) found:");
                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        logger.traceExit(festivals);
        return festivals;
    }

    @Override
    public void add(Festival entity) {
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
    public void delete(Festival entity) {
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
    public void update(Festival entity, Integer id) {
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
    public Festival findByID(Integer id) {
        logger.traceEntry("Searching festival with id: {}", id);
        Festival festival = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                festival = session.get( Festival.class, id );

                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        logger.traceExit(festival);
        return festival;
    }

    @Override
    public Iterable<Festival> findAll() {
        logger.traceEntry();
        Iterable<Festival> festivals=getAll();
        logger.traceExit(festivals);
        return festivals;
    }

    @Override
    public Collection<Festival> getAll() {
        logger.traceEntry();
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
        logger.traceExit(festivals);
        return festivals;
    }
}
