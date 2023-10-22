using System;
using model.entities;
using TicketProtobufs=networking.proto;


namespace networking;

public class ProtoUtils {

    public static DateTime longToDateTime(long units) {

        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return start.AddSeconds(units).ToLocalTime();
    }

    public static long dateTimeToLong(DateTime time)
    {
        return ((DateTimeOffset) time).ToUnixTimeSeconds();
    }

    public static User convert(TicketProtobufs.User userDTO) {
        return new User(userDTO.Username,
                userDTO.Password);
    }

    public static TicketProtobufs.User convert(User user) {
        TicketProtobufs.User builder = new TicketProtobufs.User();
        if (user.Username != null)
            builder.Username=user.Username;
        if (user.Password != null)
            builder.Password=user.Password;
        return builder;
    }

    public static Festival convert(TicketProtobufs.Festival festivalDTO) {
        return new Festival(festivalDTO.Id,
                festivalDTO.ArtistName,
                longToDateTime(festivalDTO.DateTime),
                festivalDTO.Place,
                festivalDTO.AvailableSpots,
                festivalDTO.SoldSpots);
    }

    public static TicketProtobufs.Festival convert(Festival festival) {
        TicketProtobufs.Festival builder = new TicketProtobufs.Festival();
        if (festival.Id != null)
            builder.Id=festival.Id;
        if (festival.ArtistName != null)
            builder.ArtistName=festival.ArtistName;
        if (festival.Date != null)
            builder.DateTime=dateTimeToLong(festival.Date);
        if (festival.Place != null)
            builder.Place=festival.Place;
        if (festival.AvailableSpots != null)
            builder.AvailableSpots=festival.AvailableSpots;
        if (festival.SoldSpots != null)
            builder.SoldSpots=festival.SoldSpots;
        return builder;
    }

    public static Ticket convert(TicketProtobufs.Ticket ticketDTO) {
        return new Ticket(ticketDTO.Id,
                ticketDTO.BuyerName,
                convert(ticketDTO.Festival),
                ticketDTO.NumberOfSpots);
    }

    public static void addFestivalsToResponse(TicketProtobufs.Response response,
                                              Festival[] festivals) {

        foreach (Festival festival in festivals) {
            response.Festivals.Add(convert(festival));
        }
    }

    public static TicketProtobufs.Ticket convert(Ticket ticket) {
        TicketProtobufs.Ticket builder = new TicketProtobufs.Ticket();
        if(ticket.Id!=null)
                    builder.Id=ticket.Id;
        if(ticket.BuyerName!=null)
                builder.BuyerName=ticket.BuyerName;
        if(ticket.Festival!=null)
                builder.Festival=convert(ticket.Festival);
        if(ticket.NumberOfSpots!=null)
                builder.NumberOfSpots=ticket.NumberOfSpots;
        return builder;
    }

    public static TicketProtobufs.Request createLoginRequest(User user) {
        TicketProtobufs.User userDTO = convert(user);
        return new TicketProtobufs.Request
        {
            Type = TicketProtobufs.Request.Types.Type.Login,
            User = userDTO
        };
    }

    public static TicketProtobufs.Request createLogoutRequest(User user) {
        TicketProtobufs.User userDTO = convert(user);

        return new TicketProtobufs.Request
        {
            Type = TicketProtobufs.Request.Types.Type.Logout,
            User = userDTO
        };
    }

    public static TicketProtobufs.Request createGetFestivalsRequest() {
        return new TicketProtobufs.Request
        {
            Type = TicketProtobufs.Request.Types.Type.GetFestivals
        };
    }

    public static TicketProtobufs.Request createGetFestivalsOnDateRequest(DateTime date) {
        Festival festival = new Festival(0, null, date, null,
                0, 0);
        TicketProtobufs.Festival festivalDTO = convert(festival);
        return new TicketProtobufs.Request
        {
            Type = TicketProtobufs.Request.Types.Type.GetFestivalsOnDate,
            Festival = festivalDTO
        };
    }

    public static TicketProtobufs.Request createBuyTicketRequest(Ticket ticket) {
        TicketProtobufs.Ticket ticketDTO = convert(ticket);
        return new TicketProtobufs.Request
        {
            Type = TicketProtobufs.Request.Types.Type.BuyTicket,
            Ticket = ticketDTO
        };
    }

    public static TicketProtobufs.Response createOkResponse() {
        return new TicketProtobufs.Response
        {
            Type = TicketProtobufs.Response.Types.Type.Ok,
        };
    }

    public static TicketProtobufs.Response createErrorResponse(String text) {
        return new TicketProtobufs.Response
        {
            Type = TicketProtobufs.Response.Types.Type.Error,
            Error = text
        };
    }

    public static TicketProtobufs.Response createGetFestivalsResponse(Festival[] festivals)
    {
        TicketProtobufs.Response response = new TicketProtobufs.Response
        {
            Type = TicketProtobufs.Response.Types.Type.GetFestivals
        };
        addFestivalsToResponse(response, festivals);
        return response;
    }

    public static TicketProtobufs.Response createGetFestivalsOnDateResponse(Festival[] festivals)
    {
        TicketProtobufs.Response response = new TicketProtobufs.Response
        {
            Type = TicketProtobufs.Response.Types.Type.GetFestivalsOnDate
        };
        addFestivalsToResponse(response, festivals);
        return response;
    }

    public static TicketProtobufs.Response createTicketBoughtResponse(Ticket ticket) {
        TicketProtobufs.Ticket ticketDTO = convert(ticket);
        return new TicketProtobufs.Response
        {
            Type=TicketProtobufs.Response.Types.Type.TicketBought,
            Ticket = ticketDTO
        };
    }

    public static User getUser(TicketProtobufs.Request request) {
        TicketProtobufs.User userDTO = request.User;
        return convert(userDTO);
    }

    public static String getError(TicketProtobufs.Response response) {
        return response.Error;
    }

    public static Ticket getTicket(TicketProtobufs.Response response) {
        TicketProtobufs.Ticket ticketDTO = response.Ticket;
        return convert(ticketDTO);
    }

    public static Ticket getTicket(TicketProtobufs.Request request) {
        TicketProtobufs.Ticket ticketDTO = request.Ticket;
        return convert(ticketDTO);
    }

    public static Festival getFestival(TicketProtobufs.Request request) {
        TicketProtobufs.Festival festivalDTO = request.Festival;
        return convert(festivalDTO);
    }

    public static Festival[] getFestivals(TicketProtobufs.Response response) {
        Festival[] festivals = new Festival[response.Festivals.Count];
        for (int i = 0; i < response.Festivals.Count; i++) {
            TicketProtobufs.Festival festivalDTO = response.Festivals[i];
            festivals[i] = convert(festivalDTO);
        }
        return festivals;
    }
}
