package ticketseller;

import org.hibernate.SessionFactory;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import ticketseller.model.validators.implementations.FestivalValidator;
import ticketseller.persistence.implementations.FestivalRestRepository;

@Configuration
public class FestivalRestConfig {
    @Bean
    SessionFactory sessionFactory() {
        // A SessionFactory is set up once for an application!
        final StandardServiceRegistry registry = new StandardServiceRegistryBuilder()
                .configure() // configures settings from hibernate.cfg.xml
                .build();
        try {
            return new MetadataSources( registry ).buildMetadata().buildSessionFactory();
        }
        catch (Exception e) {
            e.printStackTrace();
            System.err.println("Exception "+e);
            StandardServiceRegistryBuilder.destroy( registry );
        }
        return null;
    }

    @Bean
    FestivalRestRepository repository(){
       return new FestivalRestRepository(sessionFactory());

    }

    @Bean
    FestivalValidator validator(){
       return new FestivalValidator();
    }
}
