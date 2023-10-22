import model.ComputerRepairRequest;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import repository.AbstractRepository;

import static org.junit.jupiter.api.Assertions.assertEquals;


public class ComputerRepairRequestTest {
    @Test
    @DisplayName("One Test")
    public void testOne(){
        ComputerRepairRequest computerRepairRequest=new ComputerRepairRequest(3,"George","Suceava","0745670933","Acer","20.02.2022","it doesn't run");
        assertEquals(3,computerRepairRequest.getID());
        assertEquals("George",computerRepairRequest.getOwnerName());
        assertEquals("Suceava",computerRepairRequest.getOwnerAddress());
        assertEquals("0745670933",computerRepairRequest.getPhoneNumber());
        assertEquals("Acer",computerRepairRequest.getModel());
        assertEquals("20.02.2022",computerRepairRequest.getDate());
        assertEquals("it doesn't run",computerRepairRequest.getProblemDescription());
    }

    @Test
    @DisplayName("The Other Test")
    public void testOther(){
        ComputerRepairRequest computerRepairRequest=new ComputerRepairRequest(3,"George","Suceava","0745670933","Acer","20.02.2022","it doesn't run");
        AbstractRepository<ComputerRepairRequest,Integer> repo=new AbstractRepository<>();
        repo.add(computerRepairRequest);
        assertEquals(1,repo.getAll().size(),"Repo should have one element only");
    }
}
