import network.utils.AbstractServer;
import network.utils.ServerException;
import network.utils.SuperServiceConcurrentServer;
import org.hibernate.SessionFactory;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import ticketseller.model.validators.implementations.FestivalValidator;
import ticketseller.model.validators.implementations.TicketValidator;
import ticketseller.model.validators.implementations.UserValidator;
import ticketseller.model.validators.interfaces.IFestivalValidator;
import ticketseller.model.validators.interfaces.ITicketValidator;
import ticketseller.model.validators.interfaces.IUserValidator;
import ticketseller.persistence.implementations.*;
import ticketseller.persistence.interfaces.IFestivalRepository;
import ticketseller.persistence.interfaces.ITicketRepository;
import ticketseller.persistence.interfaces.IUserRepository;
import ticketseller.server.FestivalService;
import ticketseller.server.SuperService;
import ticketseller.server.UserService;
import ticketseller.services.interfaces.IFestivalService;
import ticketseller.services.interfaces.ISuperService;
import ticketseller.services.interfaces.IUserService;

import java.io.IOException;
import java.util.Properties;

public class StartServer {
    private static int defaultPort=55555;
    public static void main(String[] args) {

        Properties serverProps=new Properties();
        try {
            serverProps.load(StartServer.class.getResourceAsStream("/festivalserver.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find chatserver.properties "+e);
            return;
        }
        initializeORM();
        //IFestivalRepository festivalRepository=new FestivalDBRepository(serverProps);
        IFestivalRepository festivalRepository=new FestivalORMRepository(sessionFactory);
        //ITicketRepository ticketRepository=new TicketDBRepository(serverProps);
        ITicketRepository ticketRepository=new TicketORMRepository(sessionFactory);
        //IUserRepository userRepository=new UserDBRepository(serverProps);
        IUserRepository userRepository=new UserORMRepository(sessionFactory);
        IFestivalValidator festivalValidator=new FestivalValidator();
        ITicketValidator ticketValidator=new TicketValidator();
        IUserValidator userValidator=new UserValidator();

        IFestivalService festivalService=new FestivalService(festivalRepository,festivalValidator,
                ticketRepository,ticketValidator);
        IUserService userService=new UserService(userValidator,userRepository);
        ISuperService superService=new SuperService(festivalService,userService);
        int serverPort=defaultPort;
        try {
            serverPort = Integer.parseInt(serverProps.getProperty("server.port"));
        }catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number"+nef.getMessage());
            System.err.println("Using default port "+defaultPort);
        }
        System.out.println("Starting server on port: "+serverPort);
        AbstractServer server = new SuperServiceConcurrentServer(serverPort, superService);
        try {
            server.start();
        } catch (ServerException e) {
            System.err.println("Error starting the server" + e.getMessage());
        }finally {
            try {
                server.stop();
            }catch(ServerException e){
                System.err.println("Error stopping server "+e.getMessage());
            }
            if ( sessionFactory != null ) {
                sessionFactory.close();
            }
        }
    }

    static SessionFactory sessionFactory;

    static void initializeORM() {
        // A SessionFactory is set up once for an application!
        final StandardServiceRegistry registry = new StandardServiceRegistryBuilder()
                .configure() // configures settings from hibernate.cfg.xml
                .build();
        try {
            sessionFactory = new MetadataSources( registry ).buildMetadata().buildSessionFactory();
        }
        catch (Exception e) {
            e.printStackTrace();
            System.err.println("Exception "+e);
            StandardServiceRegistryBuilder.destroy( registry );
        }
    }
}
