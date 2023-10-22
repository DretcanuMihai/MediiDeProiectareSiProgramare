package network.protobuffprotocol;

import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.entities.User;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;


public class ProtoUtils {

    public static LocalDateTime longToLocalDateTime(Long units) {

        return LocalDateTime.ofInstant(Instant.ofEpochSecond(units),
                ZoneId.systemDefault());
    }

    public static Long localDateTimeToLong(LocalDateTime time) {
        return time.atZone(ZoneId.systemDefault()).toEpochSecond();
    }

    public static User convert(TicketProtobufs.User userDTO) {
        return new User(userDTO.getUsername(),
                userDTO.getPassword());
    }

    public static TicketProtobufs.User convert(User user) {
        TicketProtobufs.User.Builder builder = TicketProtobufs.User.newBuilder();
        if (user.getUsername() != null)
            builder.setUsername(user.getUsername());
        if (user.getPassword() != null)
            builder.setPassword(user.getPassword());
        return builder.build();
    }

    public static Festival convert(TicketProtobufs.Festival festivalDTO) {
        return new Festival(festivalDTO.getId(),
                festivalDTO.getArtistName(),
                longToLocalDateTime(festivalDTO.getDateTime()),
                festivalDTO.getPlace(),
                festivalDTO.getAvailableSpots(),
                festivalDTO.getSoldSpots());
    }

    public static TicketProtobufs.Festival convert(Festival festival) {
        TicketProtobufs.Festival.Builder builder = TicketProtobufs.Festival.newBuilder();
        if (festival.getId() != null)
            builder.setId(festival.getId());
        if (festival.getArtistName() != null)
            builder.setArtistName(festival.getArtistName());
        if (festival.getDateTime() != null)
            builder.setDateTime(localDateTimeToLong(festival.getDateTime()));
        if (festival.getPlace() != null)
            builder.setPlace(festival.getPlace());
        if (festival.getAvailableSpots() != null)
            builder.setAvailableSpots(festival.getAvailableSpots());
        if (festival.getSoldSpots() != null)
            builder.setSoldSpots(festival.getSoldSpots());
        return builder.build();
    }

    public static Ticket convert(TicketProtobufs.Ticket ticketDTO) {
        return new Ticket(ticketDTO.getId(),
                ticketDTO.getBuyerName(),
                convert(ticketDTO.getFestival()),
                ticketDTO.getNumberOfSpots());
    }

    public static void addFestivalsToResponse(TicketProtobufs.Response.Builder response,
                                              Festival[] festivals) {

        for (Festival festival : festivals) {
            response.addFestivals(convert(festival));
        }
    }

    public static TicketProtobufs.Ticket convert(Ticket ticket) {
        TicketProtobufs.Ticket.Builder builder = TicketProtobufs.Ticket.newBuilder();
        if(ticket.getId()!=null)
                    builder.setId(ticket.getId());
        if(ticket.getBuyerName()!=null)
                builder.setBuyerName(ticket.getBuyerName());
        if(ticket.getFestival()!=null)
                builder.setFestival(convert(ticket.getFestival()));
        if(ticket.getNumberOfSpots()!=null)
                builder.setNumberOfSpots(ticket.getNumberOfSpots());
        return builder.build();
    }

    public static TicketProtobufs.Request createLoginRequest(User user) {
        TicketProtobufs.User userDTO = convert(user);
        return TicketProtobufs.Request.newBuilder()
                .setType(TicketProtobufs.Request.Type.LOGIN)
                .setUser(userDTO).build();
    }

    public static TicketProtobufs.Request createLogoutRequest(User user) {
        TicketProtobufs.User userDTO = convert(user);

        return TicketProtobufs.Request.newBuilder()
                .setType(TicketProtobufs.Request.Type.LOGOUT)
                .setUser(userDTO).build();
    }

    public static TicketProtobufs.Request createGetFestivalsRequest() {
        return TicketProtobufs.Request.newBuilder()
                .setType(TicketProtobufs.Request.Type.GET_FESTIVALS).build();
    }

    public static TicketProtobufs.Request createGetFestivalsOnDateRequest(LocalDateTime date) {
        Festival festival = new Festival(null, null, date, null,
                null, null);
        TicketProtobufs.Festival festivalDTO = convert(festival);
        return TicketProtobufs.Request.newBuilder()
                .setType(TicketProtobufs.Request.Type.GET_FESTIVALS_ON_DATE)
                .setFestival(festivalDTO).build();
    }

    public static TicketProtobufs.Request createBuyTicketRequest(Ticket ticket) {
        TicketProtobufs.Ticket ticketDTO = convert(ticket);
        return TicketProtobufs.Request.newBuilder()
                .setType(TicketProtobufs.Request.Type.BUY_TICKET)
                .setTicket(ticketDTO).build();
    }

    public static TicketProtobufs.Response createOkResponse() {
        return TicketProtobufs.Response.newBuilder()
                .setType(TicketProtobufs.Response.Type.OK).build();
    }

    public static TicketProtobufs.Response createErrorResponse(String text) {
        return TicketProtobufs.Response.newBuilder()
                .setType(TicketProtobufs.Response.Type.ERROR)
                .setError(text).build();
    }

    public static TicketProtobufs.Response createGetFestivalsResponse(Festival[] festivals) {
        TicketProtobufs.Response.Builder response = TicketProtobufs.Response.newBuilder()
                .setType(TicketProtobufs.Response.Type.GET_FESTIVALS);
        addFestivalsToResponse(response, festivals);
        return response.build();
    }

    public static TicketProtobufs.Response createGetFestivalsOnDateResponse(Festival[] festivals) {
        TicketProtobufs.Response.Builder response = TicketProtobufs.Response.newBuilder()
                .setType(TicketProtobufs.Response.Type.GET_FESTIVALS_ON_DATE);
        addFestivalsToResponse(response, festivals);
        return response.build();
    }

    public static TicketProtobufs.Response createTicketBoughtResponse(Ticket ticket) {
        TicketProtobufs.Ticket ticketDTO = convert(ticket);
        return TicketProtobufs.Response.newBuilder()
                .setType(TicketProtobufs.Response.Type.TICKET_BOUGHT)
                .setTicket(ticketDTO).build();
    }

    public static User getUser(TicketProtobufs.Request request) {
        TicketProtobufs.User userDTO = request.getUser();
        return convert(userDTO);
    }

    public static String getError(TicketProtobufs.Response response) {
        return response.getError();
    }

    public static Ticket getTicket(TicketProtobufs.Response response) {
        TicketProtobufs.Ticket ticketDTO = response.getTicket();
        return convert(ticketDTO);
    }

    public static Ticket getTicket(TicketProtobufs.Request request) {
        TicketProtobufs.Ticket ticketDTO = request.getTicket();
        return convert(ticketDTO);
    }

    public static Festival getFestival(TicketProtobufs.Request request) {
        TicketProtobufs.Festival festivalDTO = request.getFestival();
        return convert(festivalDTO);
    }

    public static Festival[] getFestivals(TicketProtobufs.Response response) {
        Festival[] festivals = new Festival[response.getFestivalsCount()];
        for (int i = 0; i < response.getFestivalsCount(); i++) {
            TicketProtobufs.Festival festivalDTO = response.getFestivals(i);
            festivals[i] = convert(festivalDTO);
        }
        return festivals;
    }
}
