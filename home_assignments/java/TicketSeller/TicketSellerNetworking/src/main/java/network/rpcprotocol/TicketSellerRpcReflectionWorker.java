package network.rpcprotocol;

import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.entities.User;
import ticketseller.services.ServiceException;
import ticketseller.services.interfaces.ISuperService;
import ticketseller.services.interfaces.ITicketObserver;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.time.LocalDate;


public class TicketSellerRpcReflectionWorker implements Runnable, ITicketObserver {
    private ISuperService server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;
    public TicketSellerRpcReflectionWorker(ISuperService server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            connected=true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void run() {
        while(connected){
            try {
                Object request=input.readObject();
                Response response=handleRequest((Request)request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                e.printStackTrace();
            } catch (ClassNotFoundException e) {
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
            System.out.println("Error "+e);
        }
    }

    @Override
    public void updateTicketSold(Ticket ticket) {
        Response resp=new Response.Builder().type(ResponseType.TICKET_BOUGHT).data(ticket).build();
        System.out.println("Ticket bought");
        try {
            sendResponse(resp);
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }


    private static Response okResponse=new Response.Builder().type(ResponseType.OK).build();
    //  private static Response errorResponse=new Response.Builder().type(ResponseType.ERROR).build();
    private Response handleRequest(Request request){
        Response response=null;
        String handlerName="handle"+(request).type();
        System.out.println("HandlerName "+handlerName);
        try {
            Method method=this.getClass().getDeclaredMethod(handlerName, Request.class);
            response=(Response)method.invoke(this,request);
            System.out.println("Method "+handlerName+ " invoked");
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

        return response;
    }

    private Response handleLOGIN(Request request){
        System.out.println("Login request ..."+request.type());
        try {
            User user=(User)request.data();
            server.login(user.getUsername(), user.getPassword(), this);
            return okResponse;
        } catch (ServiceException e) {
            connected=false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleLOGOUT(Request request){
        System.out.println("Logout request...");
        try {
            User user=(User)request.data();
            server.logout(user.getUsername());
            connected = false;
            return okResponse;
        }catch (ServiceException e) {
            e.printStackTrace();
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleGET_FESTIVALS(Request request){
        System.out.println("GetFestivals Request ...");
        try {
            Festival[] festivals= server.getAllFestivals().toArray(new Festival[0]);
            return new Response.Builder().type(ResponseType.GET_FESTIVALS).data(festivals).build();
        } catch (ServiceException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleGET_FESTIVALS_ON_DATE(Request request){
        System.out.println("GetFestivalsOnDate Request ...");
        try {
            Festival data=(Festival)request.data();
            LocalDate date=data.getDate();
            Festival[] festivals= server.getAllFestivalsOnDate(date).toArray(new Festival[0]);
            return new Response.Builder().type(ResponseType.GET_FESTIVALS_ON_DATE).data(festivals).build();
        } catch (ServiceException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleBUY_TICKET(Request request){
        System.out.println("GetFestivalsOnDate Request ...");
        try {
            Ticket data=(Ticket) request.data();
            server.sellTicket(data.getFestival().getId(),data.getBuyerName(),data.getNumberOfSpots());
            return okResponse;
        } catch (ServiceException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private void sendResponse(Response response) throws IOException{
        System.out.println("sending response "+response);
        output.writeObject(response);
        output.flush();
    }
}
