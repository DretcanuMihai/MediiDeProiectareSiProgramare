package network.protobuffprotocol;

import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.entities.User;
import ticketseller.services.ServiceException;
import ticketseller.services.interfaces.ISuperService;
import ticketseller.services.interfaces.ITicketObserver;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.time.LocalDate;


public class ProtoWorker implements Runnable, ITicketObserver {
    private ISuperService server;
    private Socket connection;

    private InputStream input;
    private OutputStream output;
    private volatile boolean connected;

    public ProtoWorker(ISuperService server, Socket connection){
        this.server = server;
        this.connection = connection;
        try {
            output = connection.getOutputStream();//new ObjectOutputStream(connection.getOutputStream());
            input = connection.getInputStream(); //new ObjectInputStream(connection.getInputStream());
            connected = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void run() {

        while (connected) {
            try {
                // Object request=input.readObject();
                System.out.println("Waiting requests ...");
                TicketProtobufs.Request request = TicketProtobufs.Request.parseDelimitedFrom(input);
                System.out.println("Request received: " + request);
                TicketProtobufs.Response response = handleRequest(request);
                if (response != null) {
                    sendResponse(response);
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error " + e);
        }
    }

    @Override
    public void updateTicketSold(Ticket ticket) {
        System.out.println("Ticket sold " + ticket);
        try {
            sendResponse(ProtoUtils.createTicketBoughtResponse(ticket));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private TicketProtobufs.Response handleRequest(TicketProtobufs.Request request) {
        TicketProtobufs.Response response = null;
        String handlerName = "handle" + request.getType();
        System.out.println("HandlerName " + handlerName);
        try {
            Method method = this.getClass().getDeclaredMethod(handlerName, TicketProtobufs.Request.class);
            response = (TicketProtobufs.Response) method.invoke(this, request);
            System.out.println("Method " + handlerName + " invoked");
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        return response;

    }
    private static TicketProtobufs.Response okResponse=ProtoUtils.createOkResponse();

    private TicketProtobufs.Response handleLOGIN(TicketProtobufs.Request request) {
        System.out.println("Login request ..." + request.getType());
        try {
            User user = ProtoUtils.getUser(request);
            server.login(user.getUsername(), user.getPassword(), this);
            return okResponse;
        } catch (ServiceException e) {
            connected = false;
            return ProtoUtils.createErrorResponse(e.getMessage());
        }
    }

    private TicketProtobufs.Response handleLOGOUT(TicketProtobufs.Request request) {
        System.out.println("Login request ..." + request.getType());
        try {
            User user = ProtoUtils.getUser(request);
            server.logout(user.getUsername());
            connected=false;
            return okResponse;
        } catch (ServiceException e) {
            return ProtoUtils.createErrorResponse(e.getMessage());
        }
    }

    private TicketProtobufs.Response handleGET_FESTIVALS(TicketProtobufs.Request request) {
        System.out.println("GetFestivals Request ...");
        try {
            Festival[] festivals = server.getAllFestivals().toArray(new Festival[0]);
            return ProtoUtils.createGetFestivalsResponse(festivals);
        } catch (ServiceException e) {
            return ProtoUtils.createErrorResponse(e.getMessage());
        }
    }

    private TicketProtobufs.Response handleGET_FESTIVALS_ON_DATE(TicketProtobufs.Request request) {
        System.out.println("GetFestivalsOnDate Request ...");
        try {
            Festival data = ProtoUtils.getFestival(request);
            LocalDate date = data.getDate();
            Festival[] festivals = server.getAllFestivalsOnDate(date).toArray(new Festival[0]);
            return ProtoUtils.createGetFestivalsOnDateResponse(festivals);
        } catch (ServiceException e) {
            return ProtoUtils.createErrorResponse(e.getMessage());
        }
    }

    private TicketProtobufs.Response handleBUY_TICKET(TicketProtobufs.Request request) {
        System.out.println("GetFestivalsOnDate Request ...");
        try {
            Ticket data = ProtoUtils.getTicket(request);
            server.sellTicket(data.getFestival().getId(), data.getBuyerName(), data.getNumberOfSpots());
            return okResponse;
        } catch (ServiceException e) {
            return ProtoUtils.createErrorResponse(e.getMessage());
        }
    }

    private void sendResponse(TicketProtobufs.Response response) throws IOException {
        System.out.println("sending response " + response);
        response.writeDelimitedTo(output);
        //output.writeObject(response);
        output.flush();
    }
}
