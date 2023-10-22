package network.rpcprotocol.ams;

import network.rpcprotocol.Request;
import network.rpcprotocol.RequestType;
import network.rpcprotocol.Response;
import network.rpcprotocol.ResponseType;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.entities.User;
import ticketseller.services.ServiceException;
import ticketseller.services.interfaces.IAMSSuperService;
import ticketseller.services.interfaces.ISuperService;
import ticketseller.services.interfaces.ITicketObserver;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.time.LocalDate;
import java.util.Arrays;
import java.util.Collection;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;


public class TicketSellerAMSRpcProxy implements IAMSSuperService {
    private String host;
    private int port;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;
    public TicketSellerAMSRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<Response>();
    }

    @Override
    public void login(String username, String password) throws ServiceException {
        initializeConnection();
        User user=new User(username,password);
        Request req=new Request.Builder().type(RequestType.LOGIN).data(user).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.OK){
        }
        else if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            closeConnection();
            throw new ServiceException(err);
        }
    }


    @Override
    public void logout(String username) throws ServiceException {
        Request req=new Request.Builder().type(RequestType.LOGOUT).data(new User(username,null)).build();
        sendRequest(req);
        Response response=readResponse();
        closeConnection();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new ServiceException(err);
        }
    }

    @Override
    public Collection<Festival> getAllFestivals() throws ServiceException {
        Request req=new Request.Builder().type(RequestType.GET_FESTIVALS).data(null).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new ServiceException(err);
        }
        Festival[] data=(Festival[])response.data();
        Collection<Festival> toReturn= Arrays.stream(data).toList();
        return toReturn;
    }

    @Override
    public Collection<Festival> getAllFestivalsOnDate(LocalDate date) throws ServiceException {
        Festival festival=new Festival();
        festival.setDateTime(date.atTime(0,0));
        Request req=new Request.Builder().type(RequestType.GET_FESTIVALS_ON_DATE).data(festival).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new ServiceException(err);
        }
        Festival[] data=(Festival[])response.data();
        Collection<Festival> toReturn= Arrays.stream(data).toList();
        return toReturn;
    }

    @Override
    public void sellTicket(Integer festivalID, String buyerName, Integer spots) throws ServiceException {
        Festival festival=new Festival();
        festival.setId(festivalID);
        Ticket ticket=new Ticket(null,buyerName,festival,spots);
        Request req=new Request.Builder().type(RequestType.BUY_TICKET).data(ticket).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new ServiceException(err);
        }
    }

    private void closeConnection() {
        finished=true;
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendRequest(Request request)throws ServiceException {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new ServiceException("Error sending object "+e);
        }

    }

    private Response readResponse() {
        Response response=null;
        try{
            response=qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void initializeConnection(){
        try {
            connection=new Socket(host,port);
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            finished=false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }


    private void handleUpdate(Response response){
        if (response.type()== ResponseType.TICKET_BOUGHT){
            Ticket ticket=(Ticket)response.data();
            System.out.println("Tickets bought"+ticket.toString());
        }
    }

    private boolean isUpdate(Response response){
        return response.type()== ResponseType.TICKET_BOUGHT;
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    Object response=input.readObject();
                    System.out.println("response received "+response);
                    if (isUpdate((Response)response)){
                    }else{

                        try {
                            qresponses.put((Response)response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error "+e);
                } catch (ClassNotFoundException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }
}
