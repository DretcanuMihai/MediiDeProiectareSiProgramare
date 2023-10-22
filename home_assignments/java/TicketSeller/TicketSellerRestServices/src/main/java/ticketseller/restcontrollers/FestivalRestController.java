package ticketseller.restcontrollers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import ticketseller.RestException;
import ticketseller.model.entities.Festival;
import ticketseller.model.validators.implementations.FestivalValidator;
import ticketseller.persistence.implementations.FestivalRestRepository;

//@CrossOrigin
@RestController
//@RequestMapping("/ticketseller/festivals")
public class FestivalRestController {

    @Autowired
    private FestivalRestRepository festivalRepository;

    @Autowired
    private FestivalValidator festivalValidator;

    @RequestMapping(method = RequestMethod.POST)
    public Festival create(@RequestBody Festival festival) {
        System.out.println("Create:" + festival + ";");
        try {
            festivalValidator.validate(festival);
        } catch (Exception e) {
            throw new RestException(e.getMessage());
        }
        Festival result = festivalRepository.create(festival);
        if (result == null) {
            throw new RestException("Couldn't create User;\n");
        }
        return result;
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.GET)
    public Festival readById(@PathVariable Integer id) {
        System.out.println("Read by id:" + id + ";");
        try {
            festivalValidator.validateID(id);
        } catch (Exception e) {
            throw new RestException(e.getMessage());
        }
        Festival festival = festivalRepository.readById(id);
        if (festival == null) {
            throw new RestException("No festival with given id;\n");
        }
        return festival;
    }

    @RequestMapping(method = RequestMethod.GET)
    public Festival[] readAll() {
        System.out.println("Read all festivals;");
        return festivalRepository.readAll().toArray(new Festival[0]);
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.PUT)
    public Festival update(@RequestBody Festival festival) {
        System.out.println("Update festival:" + festival + ";");
        try {
            festivalValidator.validate(festival);
        } catch (Exception e) {
            throw new RestException(e.getMessage());
        }
        Festival result = festivalRepository.update(festival, festival.getId());
        if (result == null)
            throw new RestException("No festival with given id;\n");
        return result;
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.DELETE)
    public Festival delete(@PathVariable Integer id) {
        System.out.println("Deleting festival with id:" + id + ";");
        try {
            festivalValidator.validateID(id);
        }
        catch (Exception e){
            throw new RestException(e.getMessage());
        }
        Festival aux=new Festival();
        aux.setId(id);
        Festival result=festivalRepository.delete(aux);
        if(result==null){
            throw new RestException("No festival with given id;");
        }
        return result;
    }

    @ExceptionHandler(RestException.class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    public String festivalError(RestException e) {
        return e.getMessage();
    }
}
