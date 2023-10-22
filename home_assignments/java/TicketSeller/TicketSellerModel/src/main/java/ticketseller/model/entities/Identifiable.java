package ticketseller.model.entities;

public interface Identifiable<ID>{
    ID getId();

    void setId(ID id);
}
