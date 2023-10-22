package ticketseller.model.entities;

import java.io.Serializable;

public class TicketNotif implements Serializable {
    private Integer id;
    private String buyerName;
    private Integer festivalId;
    private Integer numberOfSpots;

    public TicketNotif() {
    }

    public TicketNotif(Integer id, String buyerName, Integer festivalId, Integer numberOfSpots) {
        this.id=id;
        this.buyerName = buyerName;
        this.festivalId = festivalId;
        this.numberOfSpots = numberOfSpots;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getBuyerName() {
        return buyerName;
    }

    public void setBuyerName(String buyerName) {
        this.buyerName = buyerName;
    }

    public Integer getFestivalId() {
        return festivalId;
    }

    public void setFestivalId(Integer festivalId) {
        this.festivalId = festivalId;
    }

    public Integer getNumberOfSpots() {
        return numberOfSpots;
    }

    public void setNumberOfSpots(Integer numberOfSpots) {
        this.numberOfSpots = numberOfSpots;
    }
}
