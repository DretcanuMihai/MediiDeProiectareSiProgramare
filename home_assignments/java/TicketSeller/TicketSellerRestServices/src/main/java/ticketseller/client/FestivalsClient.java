package ticketseller.client;

import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.ResourceAccessException;
import org.springframework.web.client.RestTemplate;
import ticketseller.RestException;
import ticketseller.model.entities.Festival;

import java.util.concurrent.Callable;


public class FestivalsClient {
    public static final String URL = "http://localhost:8080/ticketseller/festivals";

    private final RestTemplate restTemplate = new RestTemplate();

    private <T> T execute(Callable<T> callable) {
        try {
            return callable.call();
        } catch (ResourceAccessException | HttpClientErrorException e) { // server down, resource exception
            throw new RestException(e);
        } catch (Exception e) {
            throw new RestException(e);
        }
    }

    public Festival create(Festival festival) {
        return execute(() -> restTemplate.postForObject(URL, festival, Festival.class));
    }

    public void delete(Integer id) {
        execute(() -> {
            restTemplate.delete(String.format("%s/%s", URL, id));
            return null;
        });
    }

    public void update(Festival festival) {
        execute(() -> {
            restTemplate.put(String.format("%s/%s", URL, festival.getId()), festival);
            return null;
        });
    }

    public Festival getById(Integer id) {
        return execute(() -> restTemplate.getForObject(String.format("%s/%s", URL, id), Festival.class));
    }

    public Festival[] getAll() {
        return execute(() -> restTemplate.getForObject(URL, Festival[].class));
    }
}
