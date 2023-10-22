package ticketseller.persistence.implementations;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.User;
import ticketseller.persistence.interfaces.IUserRepository;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class UserORMRepository implements IUserRepository {
    private static final Logger logger = LogManager.getLogger();

    private final SessionFactory sessionFactory;

    public UserORMRepository(SessionFactory sessionFactory) {
        this.sessionFactory = sessionFactory;
        logger.info("Initializing UserORMRepository with sessionFactory: {} ",
                sessionFactory);
    }

    @Override
    public void add(User entity) {
        logger.traceEntry("Saving user {}", entity);
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
    public void delete(User entity) {
        logger.traceEntry("Deleting user {}", entity);
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
    public void update(User entity, String id) {
        logger.traceEntry("Updating user with id: {} with info: {}", id, entity);
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
    public User findByID(String id) {
        logger.traceEntry("Searching user with id: {}", id);
        User user = null;
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                user = session.get( User.class, id );

                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        //logger.traceExit(user);
        return user;
    }

    @Override
    public Iterable<User> findAll() {
        logger.traceEntry();
        Iterable<User> users=getAll();
        logger.traceExit(users);
        return users;
    }

    @Override
    public Collection<User> getAll() {
        logger.traceEntry();
        List<User> users = new ArrayList<>();
        try (Session session = sessionFactory.openSession()) {
            Transaction transaction = null;
            try {
                transaction = session.beginTransaction();
                users = session.createQuery("from User as u", User.class)
                        .list();
                System.out.println(users.size() + " user(s) found:");
                transaction.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la select " + ex);
                if (transaction != null)
                    transaction.rollback();
            }
        }
        logger.traceExit(users);
        return users;
    }
}
