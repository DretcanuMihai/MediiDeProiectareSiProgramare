package ticketseller.start;

import org.springframework.web.client.RestClientException;
import ticketseller.RestException;
import ticketseller.client.FestivalsClient;
import ticketseller.model.entities.Festival;

import java.time.LocalDateTime;

public class StartRestClient {
    private final static FestivalsClient FESTIVALS_CLIENT = new FestivalsClient();

    private static int toDelete = 0;

    public static void main(String[] args) {
        Festival festivalToAdd = new Festival(null, "dan sandu", LocalDateTime.now(), "clujarena", 10000, 10000);
        Festival festivalToUpdate = new Festival(51, "cata halbes", LocalDateTime.now(), "mama manu", 33, 20);
        //Festival festivalToUpdate = new Festival(51, "greu greu", LocalDateTime.now(), "mama manu", 13, 55);
        try {
            getAll();
            show(() ->{
                System.out.println("ZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
                System.out.println(FESTIVALS_CLIENT.getById(51));
                System.out.println("ZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
            });
            show(() -> {
                Festival response = FESTIVALS_CLIENT.create(festivalToAdd);
                toDelete=response.getId();
                System.out.println("##############################################");
                System.out.println(response);
                System.out.println("##############################################");
            });
            getAll();
            show(() -> FESTIVALS_CLIENT.delete(toDelete));
            getAll();
            show(() -> FESTIVALS_CLIENT.update(festivalToUpdate));
            getAll();
        } catch (RestClientException ex) {
            System.out.println("Exception ... " + ex.getMessage());
        }
    }

    private static void getAll() {
        show(() -> {
            Festival[] festivals = FESTIVALS_CLIENT.getAll();
            System.out.println("----------------------------------------------");
            for (Festival festival : festivals) {
                System.out.println(festival);
            }
            System.out.println("----------------------------------------------");
        });
    }

    private static void show(Runnable task) {
        try {
            task.run();
        } catch (RestException e) {
            System.out.println("Rest exception:" + e);
        }
    }
}
