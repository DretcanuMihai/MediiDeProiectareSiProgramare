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
import java.net.Socket;
import java.time.LocalDate;
import java.util.Arrays;
import java.util.Collection;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class ProtoProxy implements ISuperService {
    private String host;
    private int port;

    private ITicketObserver client;

    private InputStream input;
    private OutputStream output;
    private Socket connection;

    private BlockingQueue<TicketProtobufs.Response> qresponses;
    private volatile boolean finished;

    public ProtoProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<TicketProtobufs.Response>();
    }

    @Override
    public void login(String username, String password, ITicketObserver observer) throws ServiceException {
        initializeConnection();
        System.out.println("Login request ...");
        User user=new User(username,password);
        sendRequest(ProtoUtils.createLoginRequest(user));
        TicketProtobufs.Response response = readResponse();
        if (response.getType()== TicketProtobufs.Response.Type.OK){
            this.client=observer;
        }
        else if (response.getType()== TicketProtobufs.Response.Type.ERROR){
            String err=ProtoUtils.getError(response);
            closeConnection();
            throw new ServiceException(err);
        }
    }


    @Override
    public void logout(String username) throws ServiceException {
        sendRequest(ProtoUtils.createLogoutRequest(new User(username,null)));
        TicketProtobufs.Response response = readResponse();
        closeConnection();
        if (response.getType() == TicketProtobufs.Response.Type.ERROR) {
            String errorText = ProtoUtils.getError(response);
            throw new ServiceException(errorText);
        }
    }

    @Override
    public Collection<Festival> getAllFestivals() throws ServiceException {
        TicketProtobufs.Request request= ProtoUtils.createGetFestivalsRequest();
        sendRequest(request);
        TicketProtobufs.Response response=readResponse();
        if (response.getType()== TicketProtobufs.Response.Type.ERROR){
            String err=ProtoUtils.getError(response);
            throw new ServiceException(err);
        }
        Festival[] data=ProtoUtils.getFestivals(response);
        return Arrays.stream(data).toList();
    }

    @Override
    public Collection<Festival> getAllFestivalsOnDate(LocalDate date) throws ServiceException {
        TicketProtobufs.Request request= ProtoUtils.createGetFestivalsOnDateRequest(date.atStartOfDay());
        sendRequest(request);
        TicketProtobufs.Response response=readResponse();
        if (response.getType()== TicketProtobufs.Response.Type.ERROR){
            String err=ProtoUtils.getError(response);
            throw new ServiceException(err);
        }
        Festival[] data=ProtoUtils.getFestivals(response);
        return Arrays.stream(data).toList();
    }

    @Override
    public void sellTicket(Integer festivalID, String buyerName, Integer spots) throws ServiceException {
        Festival festival=new Festival();
        festival.setId(festivalID);
        Ticket ticket=new Ticket(null,buyerName,festival,spots);
        TicketProtobufs.Request req=ProtoUtils.createBuyTicketRequest(ticket);
        sendRequest(req);
        TicketProtobufs.Response response=readResponse();
        if (response.getType()== TicketProtobufs.Response.Type.ERROR){
            String err=ProtoUtils.getError(response);
            throw new ServiceException(err);
        }
    }

    private void closeConnection() {
        finished = true;
        try {
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void sendRequest(TicketProtobufs.Request request) throws ServiceException {
        try {
            System.out.println("Sending request ..." + request);
            //request.writeTo(output);
            request.writeDelimitedTo(output);
            output.flush();
            System.out.println("Request sent.");
        } catch (IOException e) {
            throw new ServiceException("Error sending object " + e);
        }

    }

    private TicketProtobufs.Response readResponse() throws ServiceException {
        TicketProtobufs.Response response = null;
        try {
            response = qresponses.take();

        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void initializeConnection(){
        try {
            connection = new Socket(host, port);
            output = connection.getOutputStream();
            //output.flush();
            input = connection.getInputStream();     //new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new ReaderThread());
        tw.start();
    }


    private void handleUpdate(TicketProtobufs.Response response) {
        if (response.getType()== TicketProtobufs.Response.Type.TICKET_BOUGHT){
            Ticket ticket=ProtoUtils.getTicket(response);
            System.out.println("Tickets bought"+ticket.toString());
            client.updateTicketSold(ticket);
        }
    }

    private class ReaderThread implements Runnable {
        public void run() {
            while (!finished) {
                try {
                    TicketProtobufs.Response response = TicketProtobufs.Response.parseDelimitedFrom(input);
                    System.out.println("response received " + response);

                    if (isUpdateResponse(response)) {
                        handleUpdate(response);
                    } else {
                        try {
                            qresponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error " + e);
                }
            }
        }
    }

    private boolean isUpdateResponse(TicketProtobufs.Response response){
        return response.getType()== TicketProtobufs.Response.Type.TICKET_BOUGHT;
    }
}
